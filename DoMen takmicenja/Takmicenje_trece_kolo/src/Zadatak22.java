import java.util.Scanner;

public class Zadatak22 {

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n, m, c;

        n = sc.nextInt();
        m = sc.nextInt();
        c = sc.nextInt();
        sc.close();

        int min = Integer.MAX_VALUE, minI = Integer.MAX_VALUE, minJ = Integer.MAX_VALUE;

        boolean working = true;

        int i = 0, j = 0;
        for(i = 0; working; i++) {
            for(j = 0; ; j++) {
                int rez = n - (m*i + c*j);
                if(rez < 0) {
                    if(-1*rez < min) {
                        min = -rez;
                        minI = i;
                        minJ = j;
                    }
                    if(j == 0) working = false;
                    break;
                }
                else if(rez < min) {
                    min = rez;
                    minI = i;
                    minJ = j;
                    if(min == 0) working = false;
                }
            }
        }

        System.out.println(minI + " " + minJ);

    }
}
