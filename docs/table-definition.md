# テーブル定義書

## Customers（顧客）

| No | 列名 | 型 | NULL | 既定値 | 説明 |
|---|---|---|---|---|---|
| 1 | Id | INT | × | IDENTITY(1,1) | 顧客ID（主キー） |
| 2 | Name | NVARCHAR(50) | × | | 顧客名 |
| 3 | Tel | NVARCHAR(20) | ○ | | 電話番号 |
| 4 | DeleteFlag | BIT | × | 0 | 削除フラグ（0:有効 / 1:削除） |
| 5 | CreatedAt | DATETIME2 | × | SYSDATETIME() | 作成日時 |
| 6 | UpdatedAt | DATETIME2 | × | SYSDATETIME() | 更新日時（更新時にアプリ側で設定） |