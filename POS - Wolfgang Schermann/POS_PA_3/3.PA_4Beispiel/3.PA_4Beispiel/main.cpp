#include <iostream>

using namespace std;


int* StrToInt(string ein, int& anz) {

    int y{};
    for (int i = 0; i < ein.size(); i++) {
        if (ein[i] >= '0' && ein[i] <= '9') {
            y++;
        }
    }

    int* arr = new int[y] {};

    int z{};
    for (int i = 0; i < ein.size(); i++) {
        if (ein[i] >= '0' && ein[i] <= '9') {
            arr[z] = ein[i] - '0';
            z++;
        }
    }
    anz = y;

    return arr;

}


int main() {

    string text = "A1B2C3";
    int textAnz{};

    int* arr = StrToInt(text, textAnz);

    for (int i = 0; i < textAnz; i++) {
        cout << arr[i] << ", ";
    }
    cout << endl;

    return 0;
}



/*

'0' = 48
'1' = 49
'2' = 50

... '0' '1' '2' '3' ...

*/