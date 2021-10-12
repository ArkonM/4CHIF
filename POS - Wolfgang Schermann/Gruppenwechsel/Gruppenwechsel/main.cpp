//Gruppenwechsel
//Schneider Armin 08.06.2020

#include <iostream>
#include <fstream>
#include <string>
#include <math.h>

using namespace std;


class Bands {
private:
	string b_name;
	int sal;
public:
	Bands(string b, int s) : b_name(b), sal(s) {};
	~Bands() {};

	string getB_name() {
		return b_name;
	};

	int getSal() {
		return sal;
	}
};



struct Node {
	int d;
	Node* next;
};

void insert(Node*& r, int data) {
	Node* n = new Node{ data, NULL };
	Node* prev = NULL, * curr = r;
	if (r == NULL) { //leere Liste - easy
		r = n;
	}
	else {
		while (curr != NULL && data > curr->d) {
			prev = curr;
			curr = curr->next;
		}
		if (prev == NULL) { //einfügen am Anfang
			n->next = curr;
			r = n;
		}
		else if (curr == NULL) { //einfügen am Ende
			prev->next = n;
		}
		else { //einfügen dazwischen
			prev->next = n;
			n->next = curr;
		}
	}
}

void print(Node* r) {
	if (r != NULL) {
		cout << r->d << endl;
		print(r->next);
	}
}

void abbauen(Node*& r) {
	if (r != NULL) {
		abbauen(r->next);
		delete r;
		r = NULL;
	}
}




void split_data(string line, string& group, int& value) {
	bool is_group = true;
	string input_value{};
	for (unsigned int i{}; i < line.length(); i++) {
		if (line[i] != ' ' && is_group == true) {
			group += line[i];
		}
		else if (line[i] != ' ' && is_group == false) {
			input_value += line[i];
		}
		else if (line[i] == ' ') {
			is_group = false;
		}
	}

	value = stoi(input_value);
}


int main() {
	string line;
	string band_name;
	string curr_band_name;
	int salary;
	int curr_salary;
	fstream file;

	file.open("text.txt", ios::in);
	if (file.is_open()) {
		getline(file, line);
		split_data(line, curr_band_name, curr_salary);

		while (!file.eof()) {
			salary = curr_salary;
			band_name = curr_band_name;
			while (!file.eof() && band_name == curr_band_name) {
				curr_band_name = "";
				curr_salary = 0;

				getline(file, line);
				split_data(line, curr_band_name, curr_salary);

				if (curr_band_name == band_name)
					salary += curr_salary;
			}
			//Klasse hier erstellen 
			cout << band_name << " : " << salary << endl;
		}
	}
}