// 2016-09-11, tencent exam
// description: 
// give a num, return how many prime number pairs (a, b) with a + b == num

#include "prime_number_sum.h"

using namespace std;

bool is_prime(int num)
{
    for(int i = 2; i < num; i++)
    {
        if(num % i == 0 && i != num)
        {
            return false;
        }
    }
    return true;
}

int get_prime_number_pair_count(int num)
{
    if(num <= 4)
    {
        return 0;
    }

    vector<bool> vec_is_prime(num, false);

    for(int i = 2; i < num; i++)
    {
        if(is_prime(i))
        {
            vec_is_prime[i] = true;
        }
    }
    int count = 0;
    for(int i = 2; i < num / 2; i++)
    {
        if(vec_is_prime[i] && vec_is_prime[num-i])
        {
            printf("%d + %d = %d\n", i, num-i, num);
            count++;
        }
    }
    return count;
}