package src;

import org.lwjgl.glfw.GLFWErrorCallback;
import org.lwjgl.opengl.GL;
import src.PeriferalListeners.ButtonState;
import src.PeriferalListeners.KeyboardListener;
import src.PeriferalListeners.MouseListener;
import src.SceneSystem.Scene;
import src.SceneSystem.TestScene;
import src.Util.Time;

import static org.lwjgl.glfw.Callbacks.glfwFreeCallbacks;
import static org.lwjgl.glfw.GLFW.*;
import static org.lwjgl.opengl.GL11.*;
import static org.lwjgl.system.MemoryUtil.NULL;

public class Window {
    private int width, height;
    private String title;
    private long glfwWindow;

    private static Window window = null;

    private Window() {
        this.width = 1280;
        this.height = 720;
        this.title = "Door";
    }

    public static Window get() {
        if(Window.window == null)
            Window.window = new Window();

        return Window.window;
    }

    public void run() {
        init();
        loop();

        glfwFreeCallbacks(glfwWindow);
        glfwDestroyWindow(glfwWindow);

        glfwTerminate();
        glfwSetErrorCallback(null).free();
    }

    public void init() {
        GLFWErrorCallback.createPrint(System.err).set();

        if(!glfwInit())
            throw new IllegalStateException("Unable to initialize GLFW");

        glfwDefaultWindowHints();
        glfwWindowHint(GLFW_VISIBLE, GLFW_FALSE);
        glfwWindowHint(GLFW_RESIZABLE, GLFW_TRUE);
        glfwWindowHint(GLFW_MAXIMIZED, GLFW_FALSE);

        glfwWindow = glfwCreateWindow(this.width, this.height, this.title, NULL, NULL);
        if(glfwWindow == NULL) throw new IllegalStateException("Failed to create window");

        glfwSetCursorPosCallback(glfwWindow, MouseListener::cursorPositionCallback);
        glfwSetMouseButtonCallback(glfwWindow, MouseListener::mouseButtonCallback);
        glfwSetKeyCallback(glfwWindow, KeyboardListener::keyCallback);

        glfwMakeContextCurrent(glfwWindow);
        glfwSwapInterval(1);

        glfwShowWindow(glfwWindow);

        GL.createCapabilities( );

        Scene.setActiveScene(new TestScene());
        Scene.getActiveScene().init();
    }

    public void loop() {
        while(!glfwWindowShouldClose(glfwWindow)) {
            Time.setStartTime();

            glfwPollEvents();

            glClearColor(1, 1, 1, 1);
            glClear(GL_COLOR_BUFFER_BIT);

            Scene.getActiveScene().update(Time.getDeltaTime());

            glfwSwapBuffers(glfwWindow);

            Time.setDeltaTime();
        }
    }
}