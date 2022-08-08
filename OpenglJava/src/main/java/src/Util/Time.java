package src.Util;

import static org.lwjgl.glfw.GLFW.glfwGetTime;

public class Time {
    private static float deltaTime = -1;
    private static float startTime;

    public static float getDeltaTime() {
        return deltaTime;
    }

    public static void setDeltaTime() {
        deltaTime = (float)glfwGetTime() - startTime;
    }

    public static void setStartTime() {
        Time.startTime = (float)glfwGetTime();
    }
}
