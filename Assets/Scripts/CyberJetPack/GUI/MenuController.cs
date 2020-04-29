using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController: MonoBehaviour
{

    protected IMenuPage m_currentPage;

    protected Stack<IMenuPage> m_pageStack = new Stack<IMenuPage>();

    protected virtual void ChangePage(IMenuPage newPage)
    {
        DeactivateCurrentPage();
        ActivateCurrentPage(newPage);
    }

    protected void DeactivateCurrentPage()
    {
        if (m_currentPage != null)
        {
            m_currentPage.hide();
        }
    }

    protected void ActivateCurrentPage(IMenuPage newPage)
    {
        m_currentPage = newPage;
        m_currentPage.show();
        m_pageStack.Push(m_currentPage);
    }

    protected void SafeBack(IMenuPage backPage)
    {
        DeactivateCurrentPage();
        ActivateCurrentPage(backPage);
    }

    public virtual void Back()
    {
        if (m_pageStack.Count == 0)
        {
            return;
        }

        DeactivateCurrentPage();
        m_pageStack.Pop();
        ActivateCurrentPage(m_pageStack.Pop());
    }

    public virtual void Back(IMenuPage backPage)
    {
        int count = m_pageStack.Count;
        if (count == 0)
        {
            SafeBack(backPage);
            return;
        }

        for (int i = count - 1; i >= 0; i--)
        {
            IMenuPage currentPage = m_pageStack.Pop();
            if (currentPage == backPage)
            {
                SafeBack(backPage);
                return;
            }
        }

        SafeBack(backPage);
    }
}
