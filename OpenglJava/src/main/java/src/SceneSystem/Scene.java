package src.SceneSystem;

import src.EntityComponentSystem.GameObject;

import java.util.LinkedList;

public abstract class Scene {

    private static Scene activeScene;
    private LinkedList<GameObject> gameObjects;

    public Scene(){
        gameObjects = new LinkedList<>();
    }

    public void init() {
        for(int i = 0; i < gameObjects.size(); i++)
            gameObjects.get(i).init();
    }

    public void update(float deltaTime) {
        for(int i = 0; i < gameObjects.size(); i++)
            gameObjects.get(i).update(deltaTime);
    }

    public static Scene getActiveScene() {
        return activeScene;
    }

    public static void setActiveScene(Scene activeScene) {
        Scene.activeScene = activeScene;
    }

    public void addGameObject(GameObject gameObject) {
        gameObjects.add(gameObject);
    }

    public void removeGameObject(GameObject gameObject) {
        for(int i = 0; i < gameObjects.size(); i++)
            if(gameObject == gameObjects.get(i)) {
                gameObjects.remove(i);
                break;
            }
    }

    public GameObject getGameObject(String name) {
        for(int i = 0; i < gameObjects.size(); i++) {
            GameObject current = gameObjects.get(i);
            if(current.getName().equals(name))
                return current;
        }
        return null;
    }
}
