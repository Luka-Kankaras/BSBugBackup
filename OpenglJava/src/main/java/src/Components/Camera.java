package src.Components;

import org.joml.Matrix4f;
import org.joml.Vector2f;
import org.joml.Vector3f;
import src.EntityComponentSystem.Component;
import src.EntityComponentSystem.GameObject;

public class Camera extends Component {

    private Matrix4f projection, view;

    public Camera(GameObject parent) {
        super(parent);
        projection = new Matrix4f();
        view = new Matrix4f();
    }

    @Override
    public void init() {
        createProjection();
    }

    @Override
    public void update(float deltaTime) {

    }

    public void createProjection() {
        projection.identity();
        projection.ortho(0.0f, 1280f, 0.0f, 720f, 0.0f, 100f);
    }

    public Matrix4f getView() {
        view.identity();
        Vector2f position = parent.getTransform().getPosition();

        view.lookAt(new Vector3f(position, 20.0f),
                    new Vector3f(position, -1.0f),
                    new Vector3f(0.0f, 1.0f, 0));

        return view;
    }

    public Matrix4f getProjection() {
        return projection;
    }
}