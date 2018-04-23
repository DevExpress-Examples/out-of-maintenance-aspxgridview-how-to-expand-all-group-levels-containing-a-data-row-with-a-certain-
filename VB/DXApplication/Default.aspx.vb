Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace DXApplication
    Partial Public Class WebForm1
        Inherits System.Web.UI.Page

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)

        End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not IsPostBack Then
                Grid.DataBind()
                ExpandGroupsByRowKeys(Grid, 74, 2, 35)
            End If
        End Sub

        Protected Sub ExpandGroupsByRowKeys(ByVal grid As ASPxGridView, ParamArray ByVal keys() As Object)
            grid.ExpandAll()

            Dim groupStack = New Stack(Of GroupInfo)(grid.GroupCount)
            Dim prevLevel = 0
            Dim visibleIndex = 0

            Do While visibleIndex <= grid.VisibleRowCount
                Dim currentLevel = grid.GetRowLevel(visibleIndex)
                If prevLevel > currentLevel Then
                    Dim groupInfo = groupStack.Pop()
                    prevLevel = groupInfo.Level
                    If Not groupInfo.Expanded Then
                        grid.CollapseRow(groupInfo.VisibleIndex)
                        visibleIndex = groupInfo.VisibleIndex + 1
                    End If
                    If visibleIndex = grid.VisibleRowCount AndAlso groupStack.Count = 0 Then
                        Return
                    End If
                    Continue Do
                End If

                prevLevel = currentLevel
                Dim isGroupRow = grid.IsGroupRow(visibleIndex)
                If isGroupRow Then
                    groupStack.Push(New GroupInfo() With {.VisibleIndex = visibleIndex, .Level = currentLevel})
                Else
                    Dim lastGroupInfo = groupStack.Peek()
                    If Not lastGroupInfo.Expanded Then
                        Dim rowKey = grid.GetRowValues(visibleIndex, grid.KeyFieldName)
                        Dim expanded = keys.Any(Function(k) Object.Equals(k, rowKey))
                        If expanded Then
                            groupStack.ToList().ForEach(Sub(g) g.Expanded = True)
                        End If
                    End If
                End If
                visibleIndex += 1
            Loop
        End Sub
    End Class

    Public Class GroupInfo
        Public Property Level() As Integer
        Public Property VisibleIndex() As Integer
        Public Property Expanded() As Boolean
    End Class

End Namespace