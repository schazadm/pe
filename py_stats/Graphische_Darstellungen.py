# -*- coding: utf-8 -*-
"""
Created on Thu Mar  2 21:13:46 2023

@author: vonwareb
"""

import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

df = pd.read_csv('time_series.csv')

print(df.head())
print(df.columns)

# plt.figure(1)
# # plt.plot(df['t'], df['x1(t)'], color='green') # Teilaufgabe_01
# plt.plot(df['t'], df['x1(t)'], color='green')  # Teilaufgabe_02
# plt.xlabel('Zeit t [s]')
# plt.ylabel('Ortsvektor r [m]')
# plt.title('Ort in Abhängigkeit der Zeit')
# # plt.title('Teilaufgabe_02: Grafische Darstellung des Ortes Zeitreihe_02')
# plt.legend(['Würfel 1'])
# plt.grid()
#
# plt.figure(2)
# # plt.plot(df['t'], df['x1(t)'], color='green') # Teilaufgabe_01
# plt.plot(df['t'], df['x2(t)'], color='green')  # Teilaufgabe_02
# plt.xlabel('Zeit t [s]')
# plt.ylabel('Ortsvektor r [m]')
# plt.title('Ort in Abhängigkeit der Zeit')
# # plt.title('Teilaufgabe_02: Grafische Darstellung des Ortes Zeitreihe_02')
# plt.legend(['Würfel 2'])
# plt.grid()
#
# plt.figure(3)
# plt.plot(df['t'], df['v1(t)'], color='purple')
# plt.xlabel('Zeit t [s]')
# plt.ylabel('Geschwindigkeit v [m/s]')
# plt.title('Geschwindigkeit in Abhängigkeit der Zeit')
# # plt.title('Teilaufgabe_02: Grafische Darstellung des Geschwindigkeit Zeitreihe_02')
# plt.legend(['Würfel 1'])
# plt.grid()
#
# plt.figure(4)
# plt.plot(df['t'], df['v2(t)'], color='purple')
# plt.xlabel('Zeit t [s]')
# plt.ylabel('Geschwindigkeit v [m/s]')
# plt.title('Geschwindigkeit in Abhängigkeit der Zeit')
# # plt.title('Teilaufgabe_02: Grafische Darstellung des Geschwindigkeit Zeitreihe_02')
# plt.legend(['Würfel 2'])
# plt.grid()
#
# plt.figure(5)
# plt.plot(df['t'], df['p1(t)'], color='red')
# plt.xlabel('Zeit t [s]')
# plt.ylabel('Impuls p [Ns]')
# plt.title('Impuls in Abhängigkeit der Zeit')
# # plt.title('Teilaufgabe_02: Grafische Darstellung des Kraft Zeitreihe_02')
# plt.legend(['Würfel 1'])
# plt.grid()
#
# plt.figure(6)
# plt.plot(df['t'], df['p2(t)'], color='red')
# plt.xlabel('Zeit t [s]')
# plt.ylabel('Impuls p [Ns]')
# plt.title('Impuls in Abhängigkeit der Zeit')
# # plt.title('Teilaufgabe_02: Grafische Darstellung des Kraft Zeitreihe_02')
# plt.legend(['Würfel 2'])
# plt.grid()

plt.figure(7)
# plt.plot(df['t'], df['x1(t)'], color='green') # Teilaufgabe_01
plt.plot(df['t'], df['x1(t)'], color='blue')  # Teilaufgabe_02
plt.plot(df['t'], df['x2(t)'], color='red')  # Teilaufgabe_02
plt.xlabel('Zeit t [s]')
plt.ylabel('Ortsvektor r [m]')
plt.title('Ort in Abhängigkeit der Zeit\nx(t)')
# plt.title('Teilaufgabe_02: Grafische Darstellung des Ortes Zeitreihe_02')
plt.legend(['Würfel 1', 'Würfel 2'])
plt.grid()

plt.figure(8)
plt.plot(df['t'], df['v1(t)'], color='blue')
plt.plot(df['t'], df['v2(t)'], color='red')
plt.xlabel('Zeit t [s]')
plt.ylabel('Geschwindigkeit v [m/s]')
plt.title('Geschwindigkeit in Abhängigkeit der Zeit\nv(t)')
# plt.title('Teilaufgabe_02: Grafische Darstellung des Geschwindigkeit Zeitreihe_02')
plt.legend(['Würfel 1', 'Würfel 2'])
plt.grid()

plt.figure(9)
plt.plot(df['t'], df['p1(t)'], color='blue')
plt.plot(df['t'], df['p2(t)'], color='red')
plt.xlabel('Zeit t [s]')
plt.ylabel('Impuls p [Ns]')
plt.title('Impuls in Abhängigkeit der Zeit\np(t)')
# plt.title('Teilaufgabe_02: Grafische Darstellung des Kraft Zeitreihe_02')
plt.legend(['Würfel 1', 'Würfel 2'])
plt.grid()

plt.show()
