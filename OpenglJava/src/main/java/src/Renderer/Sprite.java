package src.Renderer;
import org.joml.Vector2f;
import org.joml.Vector4f;
import src.Util.AssetManager;

public class Sprite {

    private Vector2f position, size;
    private Texture texture;

    // textureCoordinates: (u1, v1, u2, v2)
    private Vector4f textureCoordinates;


    public Sprite(String texturePath, Vector2f position, Vector2f size) {
        texture = AssetManager.loadTexture(texturePath);
        this.position = position;
        this.size = size;
        textureCoordinates = new Vector4f(0, 1, 1, 0);
    }

    public Sprite(Texture texture, Vector2f position, Vector2f size) {
        this.texture = texture;
        this.position = position;
        this.size = size;
        textureCoordinates = new Vector4f(0, 1, 1, 0);
    }

    public Vector2f getPosition() {
        return position;
    }

    public void setPosition(Vector2f position) {
        this.position = position;
    }

    public Vector2f getSize() {
        return size;
    }

    public void setSize(Vector2f size) {
        this.size = size;
    }

    public Texture getTexture() {
        return texture;
    }

    public void setTexture(Texture texture) {
        this.texture = texture;
    }

    public Vector4f getTextureCoordinates() {
        return textureCoordinates;
    }

    public void setTextureCoordinates(float u1, float v1, float u2, float v2) {
        textureCoordinates = new Vector4f(u1, v1, u2, v2);
    }
}
