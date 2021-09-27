using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    // 场景类型
    public enum SceneType
    {
        LoadingScene,     // 加载场景
        StartScene,       // 开始场景
        LoginScene,       // 登录场景
        MainScene,        // 主场景
        BattleScene,      // 战斗场景
    }

    // UI预制体类型
    public enum UIPrefabType
    {
        None,
        Items,
        Components,
        Panels,

        Max
    }

    // 面板枚举
    public enum PanelEnum
    {
        None,                 // 无
        LoadingPanel,         // 加载页面
        StartGamePanel,       // 开始页面
        LoginPanel,           // 登录页面  
        RegisterPanel,        // 注册页面
        CreateRolePanel,      // 创建角色页面
        MainPanel,            // 主页面
        BattlePanel,          // 战斗页面
        MapPanel,             // 地图页面
        MonsterPanel,          // 怪物地图
        NormalPanel,          // 普通地图

        ToplayerPanel,        // 顶层页面

        Max
    }

    // 面板类型
    public enum PanelType
    {
        FullScreen,         // 全屏
        Tips,               // 弹窗
        TopLayer,           // 顶层
    }

    // 数值类型
    public enum NumericalType
    {
        WinCount,            // 胜利数
        LoseCount,           // 失败数
    }

    // 事件类型
    public enum EventsType
    {
        SendBroadcast,              // 发送广播
        WinRoom,                    // 赢得房间
        UpdateWaitArea,             // 更新等待区英雄
        UpdateMonsterPanel,         // 更新打怪页面
        PassGame,                   // 通过游戏
        SucBuyHero,                 // 成功购买英雄
        MonsterAttackEnd,           // 单个怪物攻击结束
        AllMonsterAttackEnd,        // 所有怪物攻击结束
        HeroDead,                   // 英雄死亡

        Max,
    }

    // 登录结果
    public enum LoginResult
    {
        NoRegister,               // 角色未注册
        WrongPassword,            // 密码错误
        FirstLogin,               // 首次登录
        NotFirstLogin,            // 非首次登录
        RepeatLogin,              // 重复登录
    }

    // 注册结果
    public enum RegisterResult
    {
        RegisterSucc,       // 注册成功
        AccountExist,       // 账号已存在
    }

    // 创建角色结果
    public enum CreateRoleResult
    {
        CreateSucc,       // 创建成功
        NameRepeat,       // 重名
    }

    // 开始页面按钮类型
    public enum StartPanelBtnType
    {
        // 新游戏
        NewGame,
        // 继续游戏
        ContinueGame,
        // 设置
        Settings,
        // 退出
        Exit,

        Max
    }

    // 地图类型
    public enum RoomType
    {
        None,                   // 无
        Monster,                // 小怪
        Elite,                  // 精英小怪
        Boss,                   // Boss
        Shop,                   // 商店
        Event,                  // 事件

        Max,
    }

    // 常量类型
    public enum CstType
    {
        IntHP = 1,     // 玩家初始血量
        IntMoney,      // 玩家初始金币
        GridRow,       // 格子行数
        GridColumn,    // 给子列数
        MaxRoomCount,  // 最大房间数量
        ShopIntCount,  // 商店默认个数
    }

    // 游戏语言类型
    public enum GameLanguageType
    {
        ZH,         // 中文
        EN,         // 英文 
    }

    // 语言类型
    public enum LageType
    {
        CurHp = 1,            // 当前血量
        CurMoney,             // 当前金币
        CantAttack,           // 游戏未开始,不能攻击
    }

    // 英雄位置类型
    public enum HeroPosType
    {
        None,              // 无
        WaitArea,          // 等待区
        InGrid,            // 在格子内
    }

    // 回合类型
    public enum RoundOwnerType
    {
        None,
        Player,
        Monster,
    }

    // 随机利息
    public enum RandomType
    {
        None,             // 无
        DontRepeat,       // 不重复(如果随机数量超过数组最大数量会报错)
        Ignore,           // 忽略超过(如果随机数量超过数组最大数量会忽略)
    }

    #region  Buff相关

    // Buff类型
    public enum BuffType
    {
        None,          // 无
        Passtive,      // 被动

        Max            // 最大
    }

    // Buff触发阶段
    public enum BuffTgrType
    {
        None,                 // 无
        SelfRoundStart,       // 自己回合开始
        SelfRoundEnd,        // 自己回合结束
        SelfAttackStart,      // 自己攻击前
        SelfAttackEnd,        // 自己攻击前
        SelfAttackedStart,    // 自己被攻击前
        SelfAttackedEnd,      // 自己被攻击后
        SelfDeadStart,        // 自己死亡前
        SelfDeadEnd,          // 自己死亡后

        Max                   // 结束
    }

    // Buff效果类型
    public enum BuffEffectType
    {
        None,                           // 无
        /*        伤害型             */
        FixedDamage,                   // 固定伤害                   
        PerAttackDamage,                // 百分攻击伤害
        PerHPDamage,                    // 百分血量伤害


        /*        辅助型             */
        AddAttack,                      // 加攻击力
        AddAttackCount,                // 额外攻击次数

        AddHP,                          // 加血

        ResetAction,                    // 重置行动

        Poison,                         // 毒
    }

    // Buff目标类型
    public enum BuffTargetType
    {
        None,           // 无
        Self,           // 自己
        SelfAll,        // 所有队友
        Enemy,          // 敌人
        EnemyAll,     // 所有敌人
        SelfTopOne,     // 自己前面一格
    }

    // 目标主类型
    public enum TargetMainType
    {
        None,            // 无
        Friend,          // 友军
        Enemy,           // 敌军
        All,             // 所有

        Max,
    }

    // 单位类型
    public enum UnitType
    {
        None,           // 无
        Single,         // 个
        Row,            // 排
        Column,         // 列
        Around,         // 周围

        Max
    }

    // 选中类型
    public enum SelectType
    {
        None,         // 无
        Select,       // 选中TODO
        Fixed,        // 固定
        Random,       // 随机
        Self,         // 自身

        Max           // 最大
    }

    // 自身位置类型
    [Flags]
    public enum SelfPosType
    {
        None = 0,                // 无
        LT = 1 << 0,             //左上 
        MT = 1 << 1,             //前方
        RT = 1 << 2,             //右上
        LM = 1 << 3,             //左
        MM = 1 << 4,             //正中间
        RM = 1 << 5,             // 右
        LB = 1 << 6,             // 左下
        MB = 1 << 7,             // 下
        RB = 1 << 8,             // 右下

    }

    // 条件类型
    public enum ConditionType
    {
        None,                  // 无
        
    }
    #endregion
}

