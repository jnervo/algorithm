#include "link_node.h"

LinkNode* reverse_list(LinkNode* head)
{
    LinkNode *r, *p, *q;

    p = head;

    q = r = NULL;

    while(p != NULL)
    {
        q = p->next;
        p->next = r;

        r = p;
        p = q;
    }
    return r;
}

void print_list(LinkNode* head)
{
    LinkNode *p;
    p = head;

    while(p != NULL)
    {
        cout << p->data;
        p = p->next;
        if(p != NULL)
        {
            cout << "->";
        }
    }
    cout << endl << endl;
}

LinkNode* create_list(void)
{
    LinkNode *phead, *p, *q;

    phead = p = q = NULL;

    int n, val;
    cout << "please input the lengh of your list:" << endl;
    cin >> n;

    cout << "please input your data:" << endl;
    for(int i=0; i<n; i++)
    {
        p = (LinkNode *)malloc(sizeof(LinkNode));

        cin >> val;
        p->data = val;

        if(phead == nullptr)
        {
            phead = p;
        }
        else
        {
            q->next = p;
        }
        q = p;
    }
    p->next = NULL;

    cout << endl << "create successfully! your list is:" << endl;

    print_list(phead);

    return phead;
}

void test_reverse()
{
    LinkNode *list = create_list();

    LinkNode *reverslist = reverse_list(list);

    cout << endl << "reversed list is:" << endl;

    print_list(reverslist);

}