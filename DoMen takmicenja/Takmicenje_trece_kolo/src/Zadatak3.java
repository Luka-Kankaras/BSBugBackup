import java.util.LinkedList;
import java.util.Scanner;

public class Zadatak3 {

    public static long NZD(long a, long b) {
        long b0 = b, r0 = a % b;

        while(r0 != 0) {
            a = b0;
            b0 = r0;
            r0 = a % b0;
        }

        return b0;
    }

    public static long merdijan(long n) {
        LinkedList<Long> list = new LinkedList<>();

        long count = 0;
        for(long i = 1; i < n; i++) {
            if (NZD(n, i) == 1) {
                list.add(i);
                count++;
            }
        }

        if(count % 2 == 0)
            return list.get((int)(count/2 - 1));

        return list.get((int)(count/2));
    }

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        int t = sc.nextInt();
        sc.nextLine();
        long n = 0;
        long[] rez = new long[t];

        for(int i = 0; i < t; i++) {
            n = Long.parseLong(sc.nextLine());
            rez[i] = merdijan(n);
        }
        sc.close();

        for(int i = 0; i < rez.length; i++)
            System.out.println(rez[i]);
    }
}
