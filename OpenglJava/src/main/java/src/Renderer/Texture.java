package src.Renderer;

import java.nio.ByteBuffer;

import static org.lwjgl.opengl.GL11.*;
import static org.lwjgl.stb.STBImage.*;

public class Texture {
    private int rendererID;
    private String path;

    public Texture(String path) {
        this.path = path;

        rendererID = glGenTextures();
        glBindTexture(GL_TEXTURE_2D, rendererID);

        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

        int[] width = new int[1], height = new int[1], channels = new int[1];
        ByteBuffer image = stbi_load(path, width, height, channels, 0);

        if(image == null) {
            System.out.println("Couldn't load image: " + path);
            assert false;
        }

        if(channels[0] == 3) // R G B
            glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width[0], height[0], 0, GL_RGB, GL_UNSIGNED_BYTE, image);
        else if(channels[0] == 4) // R G B A
            glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, width[0], height[0], 0, GL_RGBA, GL_UNSIGNED_BYTE, image);
        else {
            System.out.println("Unknown number of channels of texture: " + path);
            assert false;
        }

        stbi_image_free(image);
    }

    public void bind() {
        glBindTexture(GL_TEXTURE_2D, rendererID);
    }

    public void unbind() {
        glBindTexture(GL_TEXTURE_2D, 0);
    }

    public int getRendererID() {
        return rendererID;
    }
}
