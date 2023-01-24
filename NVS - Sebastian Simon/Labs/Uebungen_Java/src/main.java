import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.lang.reflect.Array;
import java.util.*;
import java.util.function.Predicate;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class main {
    public static void main(String[] args) throws FileNotFoundException {
        List<Integer> Zahlen = new ArrayList<>();
        Zahlen.add(2);
        Zahlen.add(5);
        Zahlen.add(7);
        Zahlen.add(10);
        Zahlen.add(11);
        Zahlen.add(12);
        final Predicate<Integer> lowerThanFive = zahl -> zahl < 5;
        final Predicate<Integer> biggerThan10 = zahl -> zahl > 10;
        final Predicate<Integer> kombi = biggerThan10.or(lowerThanFive);
        final Predicate<String> doppelt = str ->
                Zahlen.removeIf(kombi);
        System.out.println(Zahlen);

        Stream<Integer> intStream = Zahlen.stream().filter(kombi);
        System.out.println(intStream);

        List<String> stringis = Arrays.asList("test","abc","test","hallo");
        final List<String> sortedStringis = stringis.stream().sorted(Comparator.reverseOrder()).collect(Collectors.toList());
        final List<String> sortedAndDistinctStringis = sortedStringis.stream().distinct().collect(Collectors.toList());
        System.out.println(sortedAndDistinctStringis);

        //System.out.println(sortedStringis.stream();

        List<Person> personen = new ArrayList<>();
        Person p1 = new Person();
        Person p2 = new Person();
        Person p3 = new Person();
        Person p4 = new Person();
        Person p5 = new Person();
        p1.name = "Alex"; p1.alter = 17;
        p2.name = "Sebi"; p2.alter = 17;
        p3.name = "Flo" ; p3.alter = 18;
        p4.name = "abc" ; p4.alter = 19;
        p5.name = "Lara"; p5.alter = 19;

        personen.add(p1);personen.add(p2);personen.add(p3);personen.add(p4);personen.add(p5);

        System.out.println(personen);

        Map<Boolean, List<Person>> map = personen.stream()
                .collect(Collectors.partitioningBy(Person -> Person.alter < 18));



        List<Integer> list = Arrays.asList(1,2,3,4,5,6,7,8,9);
        list.stream().forEach(System.out::print);
        System.out.println();
        list.parallelStream().forEach(System.out::print);


        String[] textKomisch;
        String textStr = "";
        List<String> text = new ArrayList<String>();
        

        int i = 0;

        File doc = new File("C:\\Users\\armin\\Downloads\\GoetheFaust.txt");
        Scanner obj = new Scanner(doc);

        while (obj.hasNextLine()) {
            textStr = textStr + obj.nextLine();
            text.add(obj.nextLine());
        }

        System.out.println(i);
        textKomisch = textStr.split(" ");
        System.out.println(textKomisch.length);

        for( String w : textKomisch){

        }
    }
}