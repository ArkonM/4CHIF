/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

 /*
  * File:   main.cpp
  * Author: samuel
  *
  * Created on March 18, 2020, 5:08 PM
  */

#include <iostream>
#include <fstream>

using namespace std;



int countNumbers(string filename) {

    int numbers{};

    ifstream file{ filename };

    if (!file) { //!file.is_open()
        throw logic_error("file could not be opened");
    }

    string line;

    while (getline(file, line)) {
        for (int i = 0; i < line.size(); i++) {
            if (line[i] >= '0' && line[i] <= '9') {
                numbers++;
            }
        }
    }

    return numbers;

}


int main(int argc, char** argv) {

    if (argc < 1) {
        cerr << "not enough arguments" << endl;
        return -1;
    }

    string filename = *(argv + 1);

    try {

        int numbers = countNumbers(filename);
        cout << numbers << endl;
    }
    catch (logic_error e) {
        cerr << e.what() << endl;
    }

    return 0;
}