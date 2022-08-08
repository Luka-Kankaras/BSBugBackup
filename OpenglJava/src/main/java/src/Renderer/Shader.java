package src.Renderer;

import org.joml.Matrix4f;
import org.lwjgl.BufferUtils;

import java.io.*;
import java.nio.FloatBuffer;
import java.nio.file.Files;
import java.nio.file.Paths;

import static org.lwjgl.opengl.GL11.GL_FALSE;
import static org.lwjgl.opengl.GL20.*;
import static org.lwjgl.opengl.GL20.glGetShaderInfoLog;

public class Shader {

    private int rendererID;
    private boolean used;

    private String path;
    private String vertexShaderSrc;
    private String fragmentShaderSrc;

    public Shader(String path) {
        vertexShaderSrc = "";
        fragmentShaderSrc = "";

        System.out.println("ALO");

        try {
            this.path = path;
            File file = new File(path);
            FileReader fileReader = new FileReader(file);
            BufferedReader br = new BufferedReader(fileReader);

            String currentLine;
            boolean writeToVertex = true;

            while((currentLine = br.readLine()) != null) {
                if(currentLine.contains("#type")) {
                    if(currentLine.contains("vertex"))
                        writeToVertex = true;
                    else if(currentLine.contains("fragment"))
                        writeToVertex = false;

                    continue;
                }

                if(writeToVertex)
                    vertexShaderSrc += currentLine + "\n";
                else
                    fragmentShaderSrc += currentLine + "\n";
            }

            compile();
        }
        catch (IOException e) {
            e.printStackTrace();
            System.out.println("ERROR: Could not open file for shader: '" + path + "'");
            assert false;
        }
    }

    public void compile() {
        int vertexId = glCreateShader(GL_VERTEX_SHADER),
                fragmentId = glCreateShader(GL_FRAGMENT_SHADER);

        glShaderSource(vertexId, vertexShaderSrc);
        glShaderSource(fragmentId, fragmentShaderSrc);

        glCompileShader(vertexId);
        glCompileShader(fragmentId);

        int success = glGetShaderi(vertexId, GL_COMPILE_STATUS);
        if(success == GL_FALSE) {
            int len = glGetShaderi(vertexId, GL_INFO_LOG_LENGTH);
            System.out.println("ERROR: Vertex shader compilation error.");
            System.out.println(glGetShaderInfoLog(vertexId, len));
            assert false;
        }

        success = glGetShaderi(fragmentId, GL_COMPILE_STATUS);
        if(success == GL_FALSE) {
            int len = glGetShaderi(fragmentId, GL_INFO_LOG_LENGTH);
            System.out.println("ERROR: Fragment shader compilation error.");
            System.out.println(glGetShaderInfoLog(fragmentId, len));
            assert false;
        }

        rendererID = glCreateProgram();
        glAttachShader(rendererID, vertexId);
        glAttachShader(rendererID, fragmentId);
        glLinkProgram(rendererID);

        success = glGetProgrami(rendererID, GL_LINK_STATUS);
        if(success == GL_FALSE) {
            int len = glGetProgrami(rendererID, GL_INFO_LOG_LENGTH);
            System.out.println("Shader program compilation error.");
            System.out.println(glGetShaderInfoLog(rendererID, len));
            assert false;
        }

        glDetachShader(rendererID, vertexId);
        glDetachShader(rendererID, fragmentId);
        glDeleteShader(vertexId);
        glDeleteShader(fragmentId);
    }

    public void use() {
        if(used) return;

        glUseProgram(rendererID);
        used = true;
    }

    public void detach() {
        if(!used) return;

        glUseProgram(0);
        used = false;
    }

    public int getRendererID() {
        return rendererID;
    }

    public void setUniformMat4(String name, Matrix4f value) {
        float[] matBuffer = new float[16];
        glUniformMatrix4fv(glGetUniformLocation(rendererID, name), false, value.get(matBuffer));
    }

    public void setUniformFloat(String name, float f0) {
        glUniform1f(glGetUniformLocation(rendererID, name), f0);
    }

    public void setUniformTexture(String name, int slot) {
        glUniform1i(glGetUniformLocation(rendererID, name), slot);
    }

    public void setUniformIntArray(String name, int[] array) {
        glUniform1iv(glGetUniformLocation(rendererID, name), array);
    }

}
