package src.PeriferalListeners;

import org.lwjgl.glfw.GLFW;

public class KeyboardListener {
    private static KeyboardListener instance;

    private ButtonState[] buttonStates;

    private KeyboardListener() {
        buttonStates = new ButtonState[350];
    }

    public static void keyCallback(long window, int key, int scancode, int action, int mods) {
        if(action == GLFW.GLFW_PRESS)
            get().buttonStates[key] = ButtonState.PRESSED;
        if(action == GLFW.GLFW_RELEASE)
            get().buttonStates[key] = ButtonState.RELEASED;
    }

    public static KeyboardListener get() {
        if(instance == null) instance = new KeyboardListener();
        return instance;
    }

    public static ButtonState getButtonState(int key) {
        return get().buttonStates[key];
    }
}
