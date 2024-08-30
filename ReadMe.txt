Stick 3020
==========

## これは何？

TRAIN CREW 用の外部ツールです。
XInput対応コントローラーの左右スティックを
それぞれ3020形のマスコンハンドルとブレーキハンドルに見立て、電車を運転できるようにします。
スティックをボタンのように使ってノッチを切り替える公式の操作方法とは違い、
スティックを倒す方向をノッチに対応させて操作します。
3020形以外も運転できます。

## 使い方

1. 操作に用いるXInput対応コントローラーを接続します。
2. TRAIN CREW および Stick 3020 を起動します。
3. TRAIN CREW の「設定 → 操作設定」で、「外部デバイス入出力」を「有効」にします。
   また、Stick 3020 で用いるコントローラーの入力を「無効」に設定します。
   ※コントローラーの入力を無効にしないと、TRAIN CREW 側でも別途入力を運転操作に反映してしまい、
     まともに操作できなくなります！
4. 環境や好みに合わせて、Stick 3020 の設定を行います。
   (使用するコントローラーを選択するなど)
5. 電車を運転します。

## 操作方法

### スティック操作

運転中は、常にスティックを傾けておくのが基本です。
スティックを戻すと、デッドマンにより電車を停止させます。(設定で無効化可能)

マスコンハンドル (左スティック) は、右上45度が N に対応します。
そこから反時計回りに回すと抑速、時計回りに回すと力行です。
30度回すごとに1段進みます。

ブレーキハンドル (右スティック) は、左がユルメ、下が常用最大、右下45度が非常に対応します。
※これらの中間の位置で判定を切り替えるため、45度よりも浅い角度で非常に入ります。
特に一旦非常ブレーキをかけると解除までに長時間かかる3020形では注意！

### その他の操作

* L1：EBリセット
* R1：連絡ブザー
* RT浅押し：電笛
* RT深押し：(電笛+)空笛

## 設定項目

* 言語 / Language
  UIの言語を選択します。
* コントローラー選択
  入力を行うコントローラーを選択します。
* ブレーキ設定
  ブレーキ操作のモードを設定します。
  基本的に「自動」を推奨しますが、3020形でデジタル操作を用いたいときや、
  新しい車種の運転でブレーキの種類が合わないときなどは、手動で設定できます。
* スティック閾値
  左右スティックをどのくらい倒していれば「倒している」という判定にするかを設定します。
* トリガー閾値 (浅)
  左右トリガーをどのくらい押し込んでいれば「浅押し」とみなすかを設定します。
* トリガー閾値 (深)
  左右トリガーをどのくらい押し込んでいれば「深押し」とみなすかを設定します。
* ノッチヒステリシス
  現在のノッチとして判定される幅が広めになるようにノッチ判定の閾値をずらすことにより、
  閾値付近でノッチがガタガタと切り替わるのを防ぐ機能において、
  閾値をどの程度ずらすかを設定します。
* デッドマン連携
  スティックが倒されていないとき、デッドマンにより電車を停止させるかを設定します。

## 関連リンク

* Stick 3020
  * https://github.com/mikecat/stick_3020
* TRAIN CREW
  * https://acty-soft.com/traincrew/
  * https://store.steampowered.com/app/1618290/TRAIN_CREW/

## ライセンス

Stick 3020 は、MITライセンスです。

```
Copyright (c) 2024 みけCAT

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

TrainCrewInput.dll (TRAIN CREW 入出力ライブラリ) は溝月レイル/Acty様の制作物であり、
MITライセンスの対象外です。
TrainCrewInput.dll の解析や改変は禁止されています。
