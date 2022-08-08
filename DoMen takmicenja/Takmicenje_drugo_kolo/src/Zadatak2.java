import java.util.LinkedList;
import java.util.Scanner;

public class Zadatak2 {

    public static void addElement(LinkedList<Integer> list, int br) {
        if(!list.contains(br))
            list.add(br);
    }

    public static int desifruj(String s) {
        LinkedList<Integer> list = new LinkedList<Integer>();
        String currBr = "";
        for(int i = 0; i < s.length(); i++) {
            char c = s.charAt(i);
            if(currBr.length() == 0) {
                if(c > '0' && c <= '9')
                    currBr += c;
            }
            else if(!(c >= '0' && c <= '9')) {
                addElement(list, Integer.parseInt(currBr));
                currBr = "";
            }
            else {
                currBr += c;
            }
        }

        if(currBr.length() > 0)
            addElement(list, Integer.parseInt(currBr));

        return list.size();
    }

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        String sifra = sc.nextLine();
        sc.close();
        System.out.println(desifruj(sifra));
    }
}
