# RuchSamochodowSI

<div>Traffic lights controled by neural network implemented into trafic emulator.</div>
<div>
<ul>
Technologies
<li>C#</li>
<li>WPF</li>
<li>Aforge.net</li>
</ul>
<div>UI show average waiting times for each direction on crosroads for full hours (current, control/previous).</div>
<div>
Road system designed as graph with FIFO queues on crossroads (T or X). 
Each queue represent diferent trafic lane with posible diferent driving direction.</dvi>
<div>It is possible to select with trafic light should be controlled by AI. All other light will change basing on schema from map.</div>
<div>Custom road map can be loaded from .json file. </div>
<div>System collect data about how many cars is on each streets and remember for "24h".
Data from last hour and from same period "day before" for each AI controled crossroad and their direct neighbours is used to determine by neural network how light should be changed.</div>
<div>Diferences between actual and previous average waiting times for full hour is used to teach AI.</div>
<div>The program takes a long time to run becouse of precalulation of neural network.</div>


<div>Not working as intended because problem in emulator part.</div>
