using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    // ��������
    public enum SceneType
    {
        LoadingScene,     // ���س���
        StartScene,       // ��ʼ����
        LoginScene,       // ��¼����
        MainScene,        // ������
        BattleScene,      // ս������
    }

    // UIԤ��������
    public enum UIPrefabType
    {
        None,
        Items,
        Components,
        Panels,

        Max
    }

    // ���ö��
    public enum PanelEnum
    {
        None,                 // ��
        LoadingPanel,         // ����ҳ��
        StartGamePanel,       // ��ʼҳ��
        LoginPanel,           // ��¼ҳ��  
        RegisterPanel,        // ע��ҳ��
        CreateRolePanel,      // ������ɫҳ��
        MainPanel,            // ��ҳ��
        BattlePanel,          // ս��ҳ��
        MapPanel,             // ��ͼҳ��
        MonsterPanel,          // �����ͼ
        NormalPanel,          // ��ͨ��ͼ

        ToplayerPanel,        // ����ҳ��

        Max
    }

    // �������
    public enum PanelType
    {
        FullScreen,         // ȫ��
        Tips,               // ����
        TopLayer,           // ����
    }

    // ��ֵ����
    public enum NumericalType
    {
        WinCount,            // ʤ����
        LoseCount,           // ʧ����
    }

    // �¼�����
    public enum EventsType
    {
        SendBroadcast,              // ���͹㲥
        WinRoom,                    // Ӯ�÷���
        UpdateWaitArea,             // ���µȴ���Ӣ��
        UpdateMonsterPanel,         // ���´��ҳ��
        PassGame,                   // ͨ����Ϸ
        SucBuyHero,                 // �ɹ�����Ӣ��
        MonsterAttackEnd,           // �������﹥������
        AllMonsterAttackEnd,        // ���й��﹥������
        HeroDead,                   // Ӣ������

        Max,
    }

    // ��¼���
    public enum LoginResult
    {
        NoRegister,               // ��ɫδע��
        WrongPassword,            // �������
        FirstLogin,               // �״ε�¼
        NotFirstLogin,            // ���״ε�¼
        RepeatLogin,              // �ظ���¼
    }

    // ע����
    public enum RegisterResult
    {
        RegisterSucc,       // ע��ɹ�
        AccountExist,       // �˺��Ѵ���
    }

    // ������ɫ���
    public enum CreateRoleResult
    {
        CreateSucc,       // �����ɹ�
        NameRepeat,       // ����
    }

    // ��ʼҳ�水ť����
    public enum StartPanelBtnType
    {
        // ����Ϸ
        NewGame,
        // ������Ϸ
        ContinueGame,
        // ����
        Settings,
        // �˳�
        Exit,

        Max
    }

    // ��ͼ����
    public enum RoomType
    {
        None,                   // ��
        Monster,                // С��
        Elite,                  // ��ӢС��
        Boss,                   // Boss
        Shop,                   // �̵�
        Event,                  // �¼�

        Max,
    }

    // ��������
    public enum CstType
    {
        IntHP = 1,     // ��ҳ�ʼѪ��
        IntMoney,      // ��ҳ�ʼ���
        GridRow,       // ��������
        GridColumn,    // ��������
        MaxRoomCount,  // ��󷿼�����
        ShopIntCount,  // �̵�Ĭ�ϸ���
    }

    // ��Ϸ��������
    public enum GameLanguageType
    {
        ZH,         // ����
        EN,         // Ӣ�� 
    }

    // ��������
    public enum LageType
    {
        CurHp = 1,            // ��ǰѪ��
        CurMoney,             // ��ǰ���
        CantAttack,           // ��Ϸδ��ʼ,���ܹ���
    }

    // Ӣ��λ������
    public enum HeroPosType
    {
        None,              // ��
        WaitArea,          // �ȴ���
        InGrid,            // �ڸ�����
    }

    // �غ�����
    public enum RoundOwnerType
    {
        None,
        Player,
        Monster,
    }

    // �����Ϣ
    public enum RandomType
    {
        None,             // ��
        DontRepeat,       // ���ظ�(��������������������������ᱨ��)
        Ignore,           // ���Գ���(������������������������������)
    }

    #region  Buff���

    // Buff����
    public enum BuffType
    {
        None,          // ��
        Passtive,      // ����

        Max            // ���
    }

    // Buff�����׶�
    public enum BuffTgrType
    {
        None,                 // ��
        SelfRoundStart,       // �Լ��غϿ�ʼ
        SelfRoundEnd,        // �Լ��غϽ���
        SelfAttackStart,      // �Լ�����ǰ
        SelfAttackEnd,        // �Լ�����ǰ
        SelfAttackedStart,    // �Լ�������ǰ
        SelfAttackedEnd,      // �Լ���������
        SelfDeadStart,        // �Լ�����ǰ
        SelfDeadEnd,          // �Լ�������

        Max                   // ����
    }

    // BuffЧ������
    public enum BuffEffectType
    {
        None,                           // ��
        /*        �˺���             */
        FixedDamage,                   // �̶��˺�                   
        PerAttackDamage,                // �ٷֹ����˺�
        PerHPDamage,                    // �ٷ�Ѫ���˺�


        /*        ������             */
        AddAttack,                      // �ӹ�����
        AddAttackCount,                // ���⹥������

        AddHP,                          // ��Ѫ

        ResetAction,                    // �����ж�

        Poison,                         // ��
    }

    // BuffĿ������
    public enum BuffTargetType
    {
        None,           // ��
        Self,           // �Լ�
        SelfAll,        // ���ж���
        Enemy,          // ����
        EnemyAll,     // ���е���
        SelfTopOne,     // �Լ�ǰ��һ��
    }

    // Ŀ��������
    public enum TargetMainType
    {
        None,            // ��
        Friend,          // �Ѿ�
        Enemy,           // �о�
        All,             // ����

        Max,
    }

    // ��λ����
    public enum UnitType
    {
        None,           // ��
        Single,         // ��
        Row,            // ��
        Column,         // ��
        Around,         // ��Χ

        Max
    }

    // ѡ������
    public enum SelectType
    {
        None,         // ��
        Select,       // ѡ��TODO
        Fixed,        // �̶�
        Random,       // ���
        Self,         // ����

        Max           // ���
    }

    // ����λ������
    [Flags]
    public enum SelfPosType
    {
        None = 0,                // ��
        LT = 1 << 0,             //���� 
        MT = 1 << 1,             //ǰ��
        RT = 1 << 2,             //����
        LM = 1 << 3,             //��
        MM = 1 << 4,             //���м�
        RM = 1 << 5,             // ��
        LB = 1 << 6,             // ����
        MB = 1 << 7,             // ��
        RB = 1 << 8,             // ����

    }

    // ��������
    public enum ConditionType
    {
        None,                  // ��
        
    }
    #endregion
}

