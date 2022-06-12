#include <iostream>

using namespace std;

//Iterativ
void selection_sort(int* arr, int length) {

	for (int i = 0; i < length; i++) {

		int l = i;

		for (int z = i; z < length; z++) {

			if (arr[z] < arr[l]) {
				l = z;
			}

		}

		int temp = arr[l];
		arr[l] = arr[i];
		arr[i] = temp;

	}

}

//End-rekursiv
void rec_selection_sort(int* arr, int length, int i = 0, int z = 0, int l = 0) {

	if (i < length) {
		if (z < length) {
			if (arr[z] < arr[l]) {
				l = z;
			}
			z++;
		}
		else {
			int temp = arr[l];
			arr[l] = arr[i];
			arr[i] = temp;

			i++;
			z = i;
			l = z;

		}
		rec_selection_sort(arr, length, i, z, l);
	}
}




int main() {

	int arr[5]{ 99, 128, 3, 30, 1 };

	selection_sort(arr, 5);

	return 0;
}