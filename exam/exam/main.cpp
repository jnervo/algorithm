#include <iostream>
#include <string>

#include "prime_number_sum.h"
#include "string_player.h"
#include "dna_repair.h"
#include "huiwen.h"
#include "link_node.h"

using namespace std;

int main()
{
    test_reverse();

    int n;

    vector<string> vec_error_code;
    vector<int> vec_edit_distance;

    while(cin >> n && n != 0)
    {
        vec_error_code.resize(n);

        string error_code;
        for(int i = 0; i < n; i++)
        {
            cin >> error_code;
            vec_error_code.push_back(error_code);
        }
        string dna_code;
        cin >> dna_code;

        vec_edit_distance.push_back(calculate_edit_distance(vec_error_code, dna_code));
    }

    int huiwen_prefix_length = get_huiwen_prefix_length("abcbaa");
    int count = get_prime_number_pair_count(100);
    char* which_direct = which_direction("atob", "a", "b");
    char* which_direct2 = which_direction("aaacaaa", "aca", "aa");
    return 0;
}