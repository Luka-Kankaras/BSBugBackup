import java.util.Scanner;

public class Zadatak1 {

    public static int inputYear(String s1) {
        int y1 = 0;
        if(s1.charAt(0) == 'A' && s1.charAt(1) == 'D') {
            y1 = Integer.parseInt(s1.substring(3));
        }
        else if(s1.charAt(s1.length()-2) == 'B' && s1.charAt(s1.length()-1) == 'C') {
            y1 = -1*Integer.parseInt(s1.substring(0, s1.length()-3));
        }
        else {
            System.out.println("?");
        }
        return y1;
    }

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        int y1 = 0, y2 = 0;
        String s1, s2;

        s1 = sc.nextLine();
        s2 = sc.nextLine();
        sc.close();

        y1 = inputYear(s1);
        y2 = inputYear(s2);

        if(y1 > 0 && y2 > 0 || y1 < 0 && y2 < 0)
            System.out.println(Math.abs(y1 - y2));
        else if(y1 < y2) {
            System.out.println(-1*(y1 - y2) - 1);
        }
        else if(y2 < y1) {
            System.out.println(-1*(y2 - y1) - 1);
        }
    }
}
