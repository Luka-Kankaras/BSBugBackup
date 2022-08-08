import java.util.Scanner;

public class Zadatak33 {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        int t = sc.nextInt();
        sc.nextLine();
        int[] rez = new int[t];

        for(int i = 0; i < t; i++) {
            int n = sc.nextInt();
            sc.nextLine();
            rez[i] = n;
        }
        sc.close();

        for(int i = 0; i < t; i++) {
            System.out.println(rez[i]);
        }
    }
}
