package src.EntityComponentSystem;

import java.util.LinkedList;
import java.util.List;

public class GameObject {

    private String name;
    private Transform transform;
    private LinkedList<Component> components;

    public GameObject(String name, Transform transform) {
        this.name = name;
        this.transform = transform;
        components = new LinkedList<>();
    }

    public void init() {
        for(int i = 0; i < components.size(); i++)
            components.get(i).init();
    }

    public void update(float deltaTime) {
        for(int i = 0; i < components.size(); i++)
            components.get(i).update(deltaTime);
    }


    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Transform getTransform() {
        return transform;
    }

    public void setTransform(Transform transform) {
        this.transform = transform;
    }

    public void addComponent(Component component) {
        components.add(component);
    }

    public void removeComponent(Component component) {
        for(int i = 0; i < components.size(); i++) {
            Component current = components.get(i);
            if(current != component) continue;
            components.remove(i);
            break;
        }
    }

    public <T extends Component> T getComponent(Class<T> componentClass) {
        for(int i = 0; i < components.size(); i++) {
            Component current = components.get(i);
            if(componentClass.isAssignableFrom(current.getClass()))
                return componentClass.cast(current);
        }
        return null;
    }

    public <T extends Component> List<T> getComponents(Class<T> componentClass) {
        LinkedList<T> componentList = new LinkedList<>();
        for(int i = 0; i < components.size(); i++) {
            Component current = components.get(i);
            if(componentClass.isAssignableFrom(current.getClass()))
                componentList.add(componentClass.cast(current));
        }

        return componentList;
    }
}
