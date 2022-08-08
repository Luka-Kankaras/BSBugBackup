package src.EntityComponentSystem;

public abstract class Component {
    protected GameObject parent;

    public Component(GameObject parent) {
        this.parent = parent;
    }

    public abstract void init();
    public abstract void update(float deltaTime);
}
