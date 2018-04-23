using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXApplication {
    public partial class WebForm1 : System.Web.UI.Page {

        protected void Page_Init(object sender, EventArgs e) {

        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Grid.DataBind();
                ExpandGroupsByRowKeys(Grid, 74, 2, 35);
            }
        }

        protected void ExpandGroupsByRowKeys(ASPxGridView grid, params object[] keys) {
            grid.ExpandAll();

            var groupStack = new Stack<GroupInfo>(grid.GroupCount);
            var prevLevel = 0;
            var visibleIndex = 0;

            while (visibleIndex <= grid.VisibleRowCount) {
                var currentLevel = grid.GetRowLevel(visibleIndex);
                if (prevLevel > currentLevel) {
                    var groupInfo = groupStack.Pop();
                    prevLevel = groupInfo.Level;
                    if (!groupInfo.Expanded) {
                        grid.CollapseRow(groupInfo.VisibleIndex);
                        visibleIndex = groupInfo.VisibleIndex + 1;
                    }
                    if (visibleIndex == grid.VisibleRowCount && groupStack.Count == 0)
                        return;
                    continue;
                }

                prevLevel = currentLevel;
                var isGroupRow = grid.IsGroupRow(visibleIndex);
                if (isGroupRow) {
                    groupStack.Push(new GroupInfo() {
                        VisibleIndex = visibleIndex,
                        Level = currentLevel
                    });
                }
                else {
                    var lastGroupInfo = groupStack.Peek();
                    if (!lastGroupInfo.Expanded) {
                        var rowKey = grid.GetRowValues(visibleIndex, grid.KeyFieldName);
                        var expanded = keys.Any(k => object.Equals(k, rowKey));
                        if (expanded)
                            groupStack.ToList().ForEach(g => g.Expanded = true);
                    }
                }
                visibleIndex++;
            }
        }
    }

    public class GroupInfo {
        public int Level { get; set; }
        public int VisibleIndex { get; set; }
        public bool Expanded { get; set; }
    }

}