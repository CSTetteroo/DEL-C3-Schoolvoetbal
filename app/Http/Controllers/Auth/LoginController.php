<?php

namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use Illuminate\Foundation\Auth\AuthenticatesUsers;
use Illuminate\Support\Facades\Http;  // Voor API-aanroepen


class LoginController extends Controller
{
    /*
    |--------------------------------------------------------------------------
    | Login Controller
    |--------------------------------------------------------------------------
    |
    | This controller handles authenticating users for the application and
    | redirecting them to your home screen. The controller uses a trait
    | to conveniently provide its functionality to your applications.
    |
    */

    use AuthenticatesUsers;

    /**
     * Where to redirect users after login.
     *
     * @var string
     */
    protected $redirectTo = '/home';

    /**
     * Create a new controller instance.
     *
     * @return void
     */
    public function __construct()
    {
        $this->middleware('guest')->except('logout');
        $this->middleware('auth')->only('logout');
    }
    public function login(Request $request)
    {
        $credentials = $request->only('email', 'password');

        // Standaard login via Laravel
        if (Auth::attempt($credentials)) {
            // Maak een API-aanroep naar de backend voor extra verificatie
            $response = Http::post('http://csharp-backend-api-url/User/login', [
                'Email' => $credentials['email'],
                'Password' => $credentials['password'],
            ]);

            if ($response->successful()) {
                return redirect()->intended($this->redirectTo)->with('message', 'Login succesvol via API!');
            } else {
                Auth::logout(); // Logout als de API-verificatie mislukt
                return back()->withErrors(['email' => 'Ongeldige inloggegevens via API.']);
            }
        }

        return back()->withErrors(['email' => 'Ongeldige inloggegevens.']);
    }
}
