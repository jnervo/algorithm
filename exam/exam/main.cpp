#include <iostream>
#include "prime_number_sum.h"
#include "string_player.h"

int main()
{
    int count = get_prime_number_pair_count(100);

    char* which_direct = which_direction("atob", "a", "b");
    char* which_direct2 = which_direction("aaacaaa", "aca", "aa");
    return 0;
}