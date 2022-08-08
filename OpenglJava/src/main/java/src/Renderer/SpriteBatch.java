package src.Renderer;

import org.joml.Matrix4f;
import org.joml.Vector2f;
import org.joml.Vector4f;
import src.Util.AssetManager;

import java.util.LinkedList;

import static org.lwjgl.opengl.GL30.*;

public class SpriteBatch {

    public int numberOfSprites;
    public int capacity = 10000;
    public boolean full = false;

    private LinkedList<Texture> textureList;
    private Shader shader;

    private Matrix4f view, projection;

    private int vertexArrayID, vertexBufferID, indexBufferID;
    private float[] vertexBuffer;
    private int[] indexBuffer;

    public SpriteBatch(String shaderPath) {
        shader = AssetManager.loadShader(shaderPath);

        if(vertexArrayID == 0) vertexArrayID = glGenVertexArrays();
        if(vertexBufferID == 0) vertexBufferID = glGenBuffers();
        if(indexBufferID == 0) indexBufferID = glGenBuffers();

        /* Vertex Layout:
            3 * float: Position,
            4 * float: Color,
            2 * float Texture Coordinates,
            1 * float: Texture ID */

        projection = new Matrix4f();
        view = new Matrix4f();

        projection.identity();
        projection.ortho(0.0f, 1280f, 0.0f, 720f, 0.0f, 100f);
    }

    public void start(){
        textureList = new LinkedList<>();
        vertexBuffer = new float[capacity * 4 * 10];
        indexBuffer = new int[capacity * 6];
        numberOfSprites = 0;
        full = false;
    }

    public void add(Sprite sprite, Vector4f color) {
        if(full) return;

        Texture texture = sprite.getTexture();
        if(!textureList.contains(texture)) textureList.add(texture);

        addVertices(sprite, texture, color);
        addIndices();

        numberOfSprites++;
        if(numberOfSprites == capacity) full = true;
    }

    public void end() {
        glBindVertexArray(vertexArrayID);

        glBindBuffer(GL_ARRAY_BUFFER, vertexBufferID);
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, indexBufferID);

        glBufferData(GL_ARRAY_BUFFER, vertexBuffer, GL_DYNAMIC_DRAW);
        glBufferData(GL_ELEMENT_ARRAY_BUFFER, indexBuffer, GL_STATIC_DRAW);

        glVertexAttribPointer(0, 3, GL_FLOAT, false, 10 * Float.BYTES, 0);
        glVertexAttribPointer(1, 4, GL_FLOAT, false, 10 * Float.BYTES, 3 * Float.BYTES);
        glVertexAttribPointer(2, 2, GL_FLOAT, false, 10 * Float.BYTES, 7 * Float.BYTES);
        glVertexAttribPointer(3, 1, GL_FLOAT, false, 10 * Float.BYTES, 9 * Float.BYTES);

        glEnableVertexAttribArray(0);
        glEnableVertexAttribArray(1);
        glEnableVertexAttribArray(2);
        glEnableVertexAttribArray(3);

        shader.use();

        shader.setUniformMat4("view", view);
        shader.setUniformMat4("projection", projection);
        shader.setUniformIntArray("uSamplers", new int[] {0, 1, 2, 3, 4, 5, 6, 7});

        for(int i = 0; i < textureList.size(); i++) {
            glActiveTexture(GL_TEXTURE0 + i);
            textureList.get(i).bind();
        }

        glDrawElements(GL_TRIANGLES, numberOfSprites * 6, GL_UNSIGNED_INT, 0);

        glDisableVertexAttribArray(0);
        glDisableVertexAttribArray(1);
        glDisableVertexAttribArray(2);
        glDisableVertexAttribArray(3);

        glBindVertexArray(0);
        glBindBuffer(GL_ARRAY_BUFFER, 0);
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

        for(int i = 0; i < textureList.size(); i++)
            textureList.get(i).unbind();

        shader.detach();
    }

    private void addVertices(Sprite sprite, Texture texture, Vector4f color) {
        Vector2f position = sprite.getPosition(), size = sprite.getSize();
        Vector4f textureCoords = sprite.getTextureCoordinates();
        float id = 0;

        // Adding vertices

        for(int i = 0; i < textureList.size(); i++) {
            if(textureList.get(i) != texture) continue;
            id = i;
            break;
        }

        float[] localVertices = {
                position.x + size.x, position.y, 0.0f,               color.x, color.y, color.z, color.w,     textureCoords.z, textureCoords.w,       id,
                position.x, position.y, 0.0f,                        color.x, color.y, color.z, color.w,     textureCoords.x, textureCoords.w,       id,
                position.x + size.x, position.y - size.y, 0.0f,      color.x, color.y, color.z, color.w,     textureCoords.z, textureCoords.y,       id,
                position.x, position.y - size.y, 0.0f,               color.x, color.y, color.z, color.w,     textureCoords.x, textureCoords.y,       id
        };

        System.arraycopy(localVertices, 0, vertexBuffer, numberOfSprites * 40, 40);
    }

    private void addIndices() {
        int lastIndex = indexBuffer[numberOfSprites * 6];
        if(lastIndex == 0) lastIndex--;

        int[] localIndices = {lastIndex + 1, lastIndex + 2, lastIndex + 3, lastIndex + 3, lastIndex + 2, lastIndex + 4};
        System.arraycopy(localIndices, 0, indexBuffer, numberOfSprites * 6, 6);
    }

    public Matrix4f getView() {
        return view;
    }

    public void setView(Matrix4f view) {
        this.view = view;
    }

    public Matrix4f getProjection() {
        return projection;
    }

    public void setProjection(Matrix4f projection) {
        this.projection = projection;
    }
}
