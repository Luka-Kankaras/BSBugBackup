import java.util.LinkedList;
import java.util.Scanner;

public class Zadatak3 {

    private static class Zelja {
        String naziv;
        int brojPonavljanja, prvoPojavljivanje;

        public Zelja(String s, int br, int prv) {
            naziv = s;
            brojPonavljanja = br;
            prvoPojavljivanje = prv;
        }

        @Override
        public String toString() {
            return naziv;
        }
    }

    public static void printList(LinkedList<Zelja> list, int k) {
        if(k > list.size()) {
            printList(list, list.size());
            return;
        }

        for (int i = 0; i < k; i++) {
            System.out.println(list.get(i));
        }
    }

    public static void addElement(LinkedList<Zelja> list, String zelja) {

        if(list.size() == 0) {
            list.add(0, new Zelja(zelja, 1, 0));
            return;
        }

        boolean found = false;
        int ind = -1;
        for(int i = 0; i < list.size(); i++) {
            if(list.get(i).naziv.compareTo(zelja) == 0) {
                list.get(i).brojPonavljanja++;
                found = true;
                ind = i;
                break;
            }
        }
        if(found) {
            for(int i = ind - 1; i >= 0; i--, ind--) {
                Zelja curr = list.get(ind), behind = list.get(i);
                if(curr.brojPonavljanja > behind.brojPonavljanja) {
                    list.set(ind, behind);
                    list.set(i, curr);
                }
                else if(curr.brojPonavljanja == behind.brojPonavljanja) {
                    if(curr.prvoPojavljivanje > behind.prvoPojavljivanje) {
                        list.set(ind, behind);
                        list.set(i, curr);
                    }
                }
                else
                    break;
            }
            return;
        }

        list.add(new Zelja(zelja, 1, list.size()));
        ind = list.size() - 1;
        for(int i = ind - 1; i >= 0; i--, ind--) {
            Zelja curr = list.get(ind), behind = list.get(i);
            if(curr.brojPonavljanja == behind.brojPonavljanja) {
                if(curr.prvoPojavljivanje > behind.prvoPojavljivanje) {
                    list.set(ind, behind);
                    list.set(i, curr);
                }
            }
            else
                break;
        }
    }

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt(), k = sc.nextInt();
        sc.nextLine();
        LinkedList<Zelja> list = new LinkedList<Zelja>();

        for(int i = 0; i < 3*n; i++) {
            String zelja = sc.nextLine();
            addElement(list, zelja);
        }
        sc.close();
        printList(list, k);
    }
}
