import java.util.Scanner;

public class Zadatak4 {

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        int t = sc.nextInt();
        sc.nextLine();
        long[] rezults = new long[t];

        for(int x = 0; x < t; x++) {
            int n = sc.nextInt(), k = sc.nextInt();
            long a = sc.nextLong(), b = sc.nextLong(), c = sc.nextLong(), d = sc.nextLong();

            long[] planets = new long[n];
            planets[0] = a;
            for(int i = 1; i < n; i++) {
                planets[i] = (b * planets[i-1] + c) % d;
            }

            for(int i = 0; i < planets.length; i++) {
                for(int j = i+1; j < planets.length; j++) {
                    if(planets[j] < planets[i]) {
                        long pp = planets[j];
                        planets[j] = planets[i];
                        planets[i] = pp;
                    }
                }
            }

            rezults[x] = planets[k - 1];
        }
        sc.close();

        for(int i = 0; i < t; i++)
            System.out.println(rezults[i]);
    }
}
