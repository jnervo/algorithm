// 2016-09-12: huiwen prefix
// description:
// input: a string, output: return the max length of its huiwen prefix

#include "huiwen.h"

using namespace std;

bool is_huiwen(char* str, int start, int end)
{
    int i = start;
    int j = end;

    while(i < j)
    {
        if(str[i] == str[j])
        {
            i++;
            j--;
        }
        else
        {
            return false; // not a huiwen
        }
    }

    return true;
}
int huiwen_length(char* str, int start, int end)
{
    int i = start;
    int j = end;

    int length = 0;
    while(i < j)
    {
        if(str[i] == str[j])
        {
            length += 2;
            i++;
            j--;
        }
        else
        {
            return -1; // not a huiwen
        }
    }

    if(i == j)
    {
        length++;
    }
    return length;
}

int get_huiwen_prefix_length(char* str)
{
    if(str == NULL)
    {
        return 0;
    }
    int len = strlen(str);
    if(len < 1)
    {
        return 0;
    }

    vector<int> vec_end_position;

    for(int j = len - 1; j >0 ; j--)
    {
        if(str[j] == str[0] && is_huiwen(str, 0, j)) 
        {
            return j + 1;
        }
    }
    return 1;
}