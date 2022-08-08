import java.time.LocalTime;
import java.util.LinkedList;
import java.util.Scanner;

public class Zadatak3 {

    private static class Subtitle {
        int id;
        LocalTime start;
        LocalTime finish;
        String text;

        public Subtitle(int id, LocalTime start, LocalTime finish, String text) {
            this.id = id;
            this.start = start;
            this.finish = finish;
            this.text = text;
        }

        private String timeToStr(LocalTime time) {
            String str = time.toString();
            String z = "";
            for(int i = 0; i < str.length(); i++) {
                if(i == str.length() - 4)
                    z += ',';
                else
                    z += str.charAt(i);
            }

            return z;
        }

        @Override
        public String toString() {
            return "" + id + "\n" + timeToStr(start) + " --> " + timeToStr(finish) + "\n" + text;
        }
    }

    public static LocalTime convertToTime(String timeStr) {
        int msec = 0, sec = 0, min = 0, hours = 0;
        int i = 0;

        StringBuilder msecStr = new StringBuilder();
        for(i = timeStr.length() - 1; i >= 0; i--) {
            char c = timeStr.charAt(i);
            if(!(c >= '0' &&  c <= '9')) {
                i--;
                break;
            }
            msecStr.insert(0, c);
        }

        StringBuilder secStr = new StringBuilder();
        for(; i >= 0; i--) {
            char c = timeStr.charAt(i);
            if(!(c >= '0' &&  c <= '9')) {
                i--;
                break;
            }
            secStr.insert(0, c);
        }

        StringBuilder minStr = new StringBuilder();
        for(; i >= 0; i--) {
            char c = timeStr.charAt(i);
            if(!(c >= '0' &&  c <= '9')) {
                i--;
                break;
            }
            minStr.insert(0, c);
        }

        StringBuilder hourStr = new StringBuilder();
        for(; i >= 0; i--) {
            char c = timeStr.charAt(i);
            hourStr.insert(0, c);
        }

        hours = Integer.parseInt(hourStr.toString());
        min = Integer.parseInt(minStr.toString());
        sec = Integer.parseInt(secStr.toString());
        msec = Integer.parseInt(msecStr.toString());

        return LocalTime.of(hours, min, sec, msec * 1000000);
    }

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        boolean whileBool = true;
        int offset = 0;

        LinkedList<Subtitle> subtitleList = new LinkedList<>();

        while(whileBool) {
            int id = sc.nextInt();
            sc.nextLine();
            String time = sc.nextLine();
            LocalTime start = convertToTime(time.substring(0, 12));
            LocalTime finish = convertToTime(time.substring(17));

            String text = "";
            String textLine = sc.nextLine();
            while(!textLine.equals("")) {
                text += textLine + "\n";
                if(textLine.equals("#")) {
                    offset = sc.nextInt();
                    whileBool = false;
                    break;
                }
                textLine = sc.nextLine();
            }

            subtitleList.add(new Subtitle(id, start, finish, text));
        }


        sc.close();

        for(int i = 0; i < subtitleList.size(); i++) {
            Subtitle s = subtitleList.get(i);
            s.start = s.start.plusNanos(offset * 1000000);
            s.finish = s.finish.plusNanos(offset * 1000000);
            System.out.println(s);
        }

    }
}
