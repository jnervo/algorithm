// 2016-09-11, tencent exam
// description: 
// give a num, return how many prime number pairs (a, b) with a + b == num

#include "prime_number_sum.h"

using namespace std;

bool isPrime(int num)
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

int getPrimeNumberPairCount(int num)
{
    if(num <= 4)
    {
        return 0;
    }

    vector<bool> vec_isPrime(num, false);

    for(int i = 2; i < num; i++)
    {
        if(isPrime(i))
        {
            vec_isPrime[i] = true;
        }
    }
    int count = 0;
    for(int i = 2; i < num; i++)
    {
        if(vec_isPrime[i] && vec_isPrime[num-i])
        {
            printf("%d + %d = %d\n", i, num-i, num);
            count++;
        }
    }
    return count;
}