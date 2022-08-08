import java.util.Scanner;

public class Zadatak1 {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int x = sc.nextInt(), y = sc.nextInt();
        sc.close();
        double alpha = (y-x)*1.0/2, beta = x - alpha;

        if(Math.floor(alpha) == alpha && alpha >= 0 && Math.floor(beta) == beta && beta >= 0)
            System.out.println("ISTINA");
        else
            System.out.println("LAZ");
    }
}
