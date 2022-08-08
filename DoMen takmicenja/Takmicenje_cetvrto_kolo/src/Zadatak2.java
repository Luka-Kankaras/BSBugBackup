import java.util.Scanner;

public class Zadatak2 {

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        int g = sc.nextInt();
        int k = sc.nextInt();

        System.out.println((int)(Math.pow(10, n-k) - 1 - g % Math.pow(10, n-k)));
    }
}
