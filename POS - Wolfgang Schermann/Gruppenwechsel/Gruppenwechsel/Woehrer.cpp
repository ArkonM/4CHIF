/*#include <iostream>
#include <fstream>
#include <string>

using namespace std;


void get_data(string input, string& group, int& value);


int main() {
    fstream infile;
    infile.open("text.txt", ios::in); //öffne Datei
    string raw_input;
    string curr_group{};
    string group;
    int curr_value;
    int group_value;


    getline(infile, raw_input); //lesen 1 Satz
    get_data(raw_input, curr_group, curr_value);

    while (infile.eof() == false) {
        group = curr_group; //Vorlauf Hauptgruppe
        group_value = curr_value;
        while (infile.eof() == false //nicht Dateiende
            && curr_group == group //in gleicher Gruppe
            ) {
            curr_group = "";
            curr_value = 0;
            getline(infile, raw_input); //lese nächsten Satz
            get_data(raw_input, curr_group, curr_value);

            if (curr_group == group) {
                group_value += curr_value;
            }
        }

        cout << group << " : " << group_value << endl;  //Nachlauf Hauptgruppe    
    }

    infile.close();
    return 0;
}


void get_data(string input, string& group, int& value) {
    bool is_group = true;
    string raw_value{};
    for (unsigned int i{}; i < input.length(); i++) {
        if (input[i] != ' ' && is_group == true) {
            group += input[i];
        }
        else if (input[i] != ' ' && is_group == false) {
            raw_value += input[i];
        }
        else if (input[i] == ' ') {
            is_group = false;
        }
    }

    value = stoi(raw_value);
}*/