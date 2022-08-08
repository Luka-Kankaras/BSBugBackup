package src.SceneSystem;

import org.joml.Vector2f;
import org.joml.Vector4f;
import src.Components.Camera;
import src.EntityComponentSystem.GameObject;
import src.EntityComponentSystem.Transform;
import src.Renderer.Shader;
import src.Renderer.Sprite;
import src.Renderer.SpriteBatch;
import src.Renderer.Texture;

import java.awt.*;

import static org.lwjgl.opengl.GL30.*;

public class TestScene extends Scene {

    private SpriteBatch spriteBatch;
    private Sprite sprite;

    @Override
    public void init() {
        GameObject cameraObject = new GameObject("Main camera", new Transform());
        cameraObject.addComponent(new Camera(cameraObject));

        addGameObject(cameraObject);
        super.init();

        spriteBatch = new SpriteBatch("assets/shaders/testShader.glsl");
        spriteBatch.setProjection(cameraObject.getComponent(Camera.class).getProjection());
        sprite = new Sprite("assets/textures/tex2.jpg", new Vector2f(0, 100), new Vector2f(100, 100));
    }

    @Override
    public void update(float deltaTime) {
        Camera camera = getGameObject("Main camera").getComponent(Camera.class);
        spriteBatch.setView(camera.getView());

        spriteBatch.start();

        for(int i = 0; i < spriteBatch.capacity; i++)
            spriteBatch.add(sprite, new Vector4f(1, 1, 1, 1));

        spriteBatch.end();

        System.out.println("FPS: " + (1 / deltaTime));
    }
}
