# HelloCore

ASP.NET Core（Razor Pages）と SQL Server で作成した、顧客管理のサンプルアプリケーションです。

## 作成の目的

VB.NET での業務システム開発の経験をもとに、ASP.NET Core / C# による Web アプリケーション開発を実践するために作成しました。

## 使用技術

- C# / ASP.NET Core 8（Razor Pages）
- SQL Server（LocalDB）
- ADO.NET（Microsoft.Data.SqlClient）
- Bootstrap

## 機能

- 顧客一覧の表示
- 顧客の新規登録（入力チェック付き）
- 顧客情報の編集
- 顧客の削除（論理削除）

## 工夫した点

- パラメータ化クエリによる SQL インジェクション対策
- 削除フラグ（DeleteFlag）による論理削除
- 作成日時・更新日時（CreatedAt / UpdatedAt）の記録
- 登録・更新後のリダイレクトによる二重送信の防止
- 開発環境は Windows 認証の LocalDB を使用し、接続文字列に認証情報を含まない構成

## 実行方法

1. Visual Studio 2022 で `HelloCore.sln` を開く
2. LocalDB（`(localdb)\MSSQLLocalDB`）に `PortfolioDb` を作成し、`Database/create_tables.sql` を実行する
3. F5 キーで実行する

## 今後の予定

- 検索・ページング機能
- ログイン機能
- Entity Framework Core を使った画面の追加
