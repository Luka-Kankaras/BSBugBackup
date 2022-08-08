public class Zadatak1v2 {
    public static void printNiz(int[] arr) {
        for(int i = 0; i < arr.length; i++)
            System.out.print(arr[i] + " ");
        System.out.println();
    }

    public static void main(String[] args) {
        int n = 21;

        int[] arr = new int[n];
        for(int i = 1; i <= n; i++)
            arr[i-1] = i;

        printNiz(arr);
    }
}
