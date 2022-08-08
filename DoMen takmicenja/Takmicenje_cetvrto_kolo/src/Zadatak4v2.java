import java.util.LinkedList;
import java.util.Scanner;

public class Zadatak4v2 {

    public static void printList(LinkedList<Long> list) {
        for(int i = 0; i < list.size(); i++)
            System.out.print(list.get(i) + " ");
        System.out.println();
    }

    public static int addSortToList(LinkedList<Long> list, long a) {
        if(list.size() == 0) {
            return 0;
        }
        int i = 0;
        for(i = 0; i < list.size(); i++) {
            if(a > list.get(i)) continue;
            else
                return i;
        }
        return i;
    }


    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        int t = sc.nextInt();
        sc.nextLine();
        long[] rezults = new long[t];

        for(int x = 0; x < t; x++) {
            int n = sc.nextInt(), k = sc.nextInt();
            long a = sc.nextLong(), b = sc.nextLong(), c = sc.nextLong(), d = sc.nextLong();

            LinkedList<Long> planets = new LinkedList<>();
            planets.add(a);
            long[] planetArr = new long[n];
            planetArr[0] = a;

            for(int i = 1; i < n; i++) {
                long newPlanet = (b * planetArr[i-1] + c) % d;
                planetArr[i] = newPlanet;
                planets.add(addSortToList(planets, newPlanet), newPlanet);
            }

            rezults[x] = planets.get(k - 1);
        }
        sc.close();

        for(int i = 0; i < t; i++)
            System.out.println(rezults[i]);
    }
}
