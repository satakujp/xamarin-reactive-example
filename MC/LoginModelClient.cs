
using RestSharp.Portable;
using RestSharp.Portable.Deserializers;
using RestSharp.Portable.HttpClient;
using System;
using System.Threading.Tasks;

namespace MC
{
	/// <summary>
	/// RESTサーバーへ問い合わせをするためのクラス
	/// RESTサーバーへのリクエストはRestSharpを使用する。
	/// </summary>
    public class LoginModelClient
    {
        public LoginModelClient()
        {
            _client = new RestClient("http://absence.dev.mstatic.ru/api/v1");
        }

		/// <summary>
		/// RESTサーバへ非同期で問い合わせて認証情報を取得する。
		/// `async`キーワードを付けることで非同期で実行される。
		/// </summary>
		/// <returns>The auth async.</returns>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
        public async Task<AuthToken> GetAuthAsync(string username, string password)
        {
			var request = new RestRequest("auth/token", Method.POST);
            request.AddBody(
                new {
                    login = username,
                    password = password
                });

			/// `await`キーワードでサーバーからの問い合わせが来るまで待つ。
			/// どうも詳しくは調べていないが、BaseModelオブジェクトにうまく変換されるような応答が返されるようだ。
			var result = await _client.Execute<BaseModel<AuthToken>>(request);

            return result.Data.data;
        }

        protected IRestClient _client;
    }
}

