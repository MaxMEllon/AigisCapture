# AigisCapture

[![BuildStatus](https://img.shields.io/appveyor/ci/maxmellon/AigisCapture/master.svg?style=flat-square)](https://ci.appveyor.com/project/MaxMEllon/attereco-front/branch/master)
[![Release](https://img.shields.io/github/release/MaxMEllon/AigisCapture.svg?style=flat-square)](https://github.com/MaxMEllon/AigisCapture/releases/latest)
[![License](https://img.shields.io/github/license/MaxMEllon/AigisCapture.svg?style=flat-square)](https://github.com/MaxMEllon/AigisCapture/blob/master/LICENSE.txt)

<p align="center">
  <img src="https://raw.githubusercontent.com/MaxMEllon/AigisCapture/logo/ui.PNG">
</p>

## About

DMMにて提供されているタワーディフェンス型ゲーム「千年戦争アイギス」の画面を
手軽にスクリーンショットするためのアプリケーションです．

## Feature

- スクリーンショット保存
  - 画像認識を用いたアイギス画面の追従
  - 名前無しオプション
  - 保存場所指定

## Requirements

### Enviroment

- .NET Framework 4.5.1
- C# 6.0

### Library

- [MVVMLightToolKit](http://www.mvvmlight.net/)
  - **用途** : MVVM(Model/View/ViewModel)パターン用インフラストラクチャ
  - **ライセンス** : The MIT License (MIT)

- [OpenCVSharp](https://github.com/shimat/opencvsharp)
  - **用途** : アイギス画面追従のための画像認識
  - **ライセンス** : BSD 3-Clause License

## Reference

- [WPF で Visual Studio 2012 のような光るウィンドウを作る](http://grabacr.net/archives/507)
- [添付ビヘイビアを使い TextBox で数値以外を入力できなくする。](http://d.hatena.ne.jp/hilapon/20101021/1287641423)

## LICENSE

This software is released under the MIT License, see LICENSE.txt.

