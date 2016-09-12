#include <iostream>
#include "prime_number_sum.h"
#include "string_player.h"
#include "huiwen.h"

int main()
{
    int huiwen_prefix_length = get_huiwen_prefix_length("abcbaa");

    int count = get_prime_number_pair_count(100);

    char* which_direct = which_direction("atob", "a", "b");
    char* which_direct2 = which_direction("aaacaaa", "aca", "aa");
    return 0;
}