﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;

namespace NewLife.OA.Web.Areas.Project.Controllers
{
    [DisplayName("任务")]
    public class TaskController : EntityController<WorkTask>
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.HeaderTitle = "";
            ViewBag.HeaderContent = "";

            base.OnActionExecuting(filterContext);
        }

        static TaskController()
        {
            // 过滤要显示的字段
            var names = "ID,Name,Score,TaskPriority,TaskStatus,Progress,MasterName,PlanStartTime,PlanEndTime,PlanCost,StartTime,UpdateUserName,UpdateTime".Split(",");
            var fs = WorkTask.Meta.AllFields;
            var list = names.Select(e => fs.FirstOrDefault(f => f.Name.EqualIgnoreCase(e))).Where(e => e != null);
            //list.RemoveAll(e => !names.Contains(e.Name));
            ListFields.Clear();
            ListFields.AddRange(list);
        }

        /// <summary>表单页视图。子控制器可以重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override ActionResult FormView(WorkTask entity)
        {
            // 如果是新增任务，那么路由的id就是父任务ID，这里处理一下
            if (entity.ID == 0)
            {
                entity.ParentID = RouteData.Values["id"].ToInt();
            }

            return base.FormView(entity);
        }

        public override ActionResult Add(WorkTask entity)
        {
            // 如果是新增任务，那么路由的id就是父任务ID，这里处理一下
            entity.ParentID = RouteData.Values["id"].ToInt();
            entity.ID = 0;

            return base.Add(entity);
        }
    }
}