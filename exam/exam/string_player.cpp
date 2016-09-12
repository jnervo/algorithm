// 2016-09-11, 360 exam
// description: 
// input: full_string, string_A, string_B
// output: if you can find string_A, string_B from full_string? Forward, Backward or Both?

#include "string_player.h"

int str_find(const char *s, const char *t, const int start_index)
{
    int i, j, k;
    for (i = start_index; s[i] != '\0'; i++) 
    {
        for (j=i, k=0; t[k]!='\0' && s[j]==t[k]; j++, k++);
        if (k > 0 && t[k] == '\0')
            return i;
    }
    return -1;
}

void str_reverse(char* str)
{
    int len = strlen(str);
    for(size_t i=0;i<strlen(str)/2;i++)
    {
        char temp=str[i];
        str[i]=str[len-i-1];
        str[len-i-1]=temp;
    }
}

char* get_str_reverse(const char *str)
{
    char* tmp = new char[strlen(str)];
    strcpy_s(tmp, strlen(str)+1, str);
    str_reverse(tmp);
    return tmp;
}

bool can_find(char* fullStr, char* strA, char* strB)
{
    int index = str_find(fullStr, strA, 0);
    if(index >=0 && (str_find(fullStr, strB, index + 1) >= 0))
    {
        return true;
    }
    return false;
}

char* which_direction(char* fullStr, char* strA, char* strB)
{
    bool forward = can_find(fullStr, strA, strB);
    bool backward = can_find(get_str_reverse(fullStr), strA, strB);

    if(forward && backward)
    {
        return "BOTH";
    }
    else if(forward)
    {
        return "FORWARD";
    }
    else if(backward)
    {
        return "BACKWARD";
    }
    return "NONE";
}
