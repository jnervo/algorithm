#include <stdlib.h>
#include <iostream>

using namespace std;

typedef struct node
{
    int data;
    struct node* next;
}LinkNode, *LinkList; 

// reverse the list
LinkNode* reverse_list(LinkNode* head);

// print the list
void print_list(LinkNode* head);

// creat the list by read user's input
LinkNode* create_list(void);

// function for main to test reverse
void test_reverse();