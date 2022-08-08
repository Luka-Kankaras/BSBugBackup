import java.util.LinkedList;
import java.util.Scanner;

public class Zadatak1 {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        int n = sc.nextInt(), max = 0, currIteration = 1, currListSize = 0;
        sc.close();

        LinkedList<Integer> list = new LinkedList<Integer>();
        for(int i = 1; i < n + 1; i++) {
            list.add(i);
            currListSize++;
        }

        while(currListSize > 1) {
            LinkedList<Integer> newList = new LinkedList<Integer>();

            int count = 0, currMax = 0;
            for(int i = 0; i < list.size(); i+= currIteration + 1) {
                currMax = list.get(i);
                newList.add(currMax);
                count++;
            }

            if(count > 1)
                max = currMax;

            currListSize = count;
            list = newList;
            currIteration++;
        }

        System.out.println(max);
    }
}
