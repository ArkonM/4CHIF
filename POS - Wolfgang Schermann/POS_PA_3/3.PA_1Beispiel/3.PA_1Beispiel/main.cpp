#include <iostream>
#include <stdexcept>

using namespace std;



struct Node {

	int data;
	Node* next;

};



int finde(Node* node, int n) {

	int last{ -1 };

	for (int i = 0; node != nullptr; i++) {

		if (node->data == n) {
			last = i;
		}

		node = node->next;

	}

	if (last == -1) {
		throw logic_error("nicht enthalten");
	}

	return last;

}

void abbauen(Node*& n) {

	if (n != nullptr) {
		del_rek(temp);
		delete n;
		n = nullptr;
	}

}


int main() {

	Node* root = new Node{ 1, new Node{2, new Node{3, new
				  Node{4, new Node{5, nullptr}}}} };
	try {
		cout << finde(root, 10) << endl;
	}
	catch (logic_error e) {
		cerr << "error" << endl;
	}



}
