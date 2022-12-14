# 钢铁雄心4随机MOD生成器

## 可自定义的数值

- ### State 文件

    - #### 资源 (Resources)

        - [x] 最大值
        - [x] 最小值
        - [x] 每种资源的出现概率

    - #### 建筑物 (Buildings)

        - [x] 最大值
        - [ ] 每种建筑的出现概率

    - #### 其他

        - [x] 人口 (Manpower)
        - [x] State类型 (StateCategory)

- ### 国家

    - [ ] 随机生成的国策 (随机的效果, 位置, 天数等 (超大的饼))
    - [ ] 科研槽
    - [ ] 初始政治点
    - [ ] 初始指挥点数
    - [ ] 初始运输船数
    - [ ] 领导人特性 (效果, 图片)
    - [ ] 初始民族精神
    - [ ] 傀儡关系 (傀儡等级)
    - [ ] 同盟关系
    - [ ] 战争状态
    - [ ] 内阁 (效果, 图片, 花费)
    - [ ] 国家名称

- ### 战争

    - [ ] 针对某一个国家的攻防修正
    - [ ] 陆军部队 (部队类型, 数量)
    - [ ] 空军部队
    - [ ] 海军部队

## 技术栈

- .NET 7 + WPF
- HOI4文件解析库: [CWTools](https://github.com/cwtools/cwtools)
- 数学库: [MathNet.Numerics](https://numerics.mathdotnet.com/)
- 日志框架: [NLog](https://nlog-project.org/)
- JSON库: [Newtonsoft.Json](https://www.newtonsoft.com/json)
