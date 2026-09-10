import type { SessionUser } from '../utils/session';
import { useState, type FormEvent } from 'react';
import { Building2, Mail, Lock, Eye, EyeOff, ArrowRight, AlertCircle, ShieldCheck } from 'lucide-react';
import './LoginPage.css';

interface LoginPageProps {
  onLoginSuccess?: (user: SessionUser) => void;
}

export function LoginPage({ onLoginSuccess }: LoginPageProps) {
  const [email, setEmail] = useState<string>(() => {
    return localStorage.getItem('jld_remember_email') || '';
  });
  const [password, setPassword] = useState<string>('');
  const [showPassword, setShowPassword] = useState<boolean>(false);
  const [rememberMe, setRememberMe] = useState<boolean>(() => {
    return !!localStorage.getItem('jld_remember_email') || false;
  });
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setErrorMessage(null);

    const trimmedEmail = email.trim().toLowerCase();
    if (!trimmedEmail) {
      setErrorMessage('Please enter your email address.');
      return;
    }

    if (!trimmedEmail.includes('@') || !trimmedEmail.includes('.')) {
      setErrorMessage('Please enter a valid email address.');
      return;
    }

    if (!password) {
      setErrorMessage('Please enter your password.');
      return;
    }

    setIsSubmitting(true);
    try {
      const response = await fetch('/api/auth/login', { 
        method: 'POST', 
        headers: { 'Content-Type': 'application/json' }, 
        body: JSON.stringify({ email: trimmedEmail, password }) 
      }).catch(() => null);

      if (response && response.ok) {
        const result = await response.json().catch(() => null);
        if (result?.user) {
          if (rememberMe) localStorage.setItem('jld_remember_email', trimmedEmail);
          else localStorage.removeItem('jld_remember_email');
          sessionStorage.setItem('jld_auth_user', JSON.stringify(result.user));
          setPassword('');
          onLoginSuccess?.(result.user);
          return;
        }
      }

      // If the backend responded with invalid credentials or rate limiting, report it directly
      if (response && (response.status === 401 || response.status === 400 || response.status === 429)) {
        const result = await response.json().catch(() => null);
        throw new Error(result?.error || 'Email or password is incorrect.');
      }

      // Offline / standalone Vite fallback: if backend API is not running (e.g. 404 or fetch failure)
      if (trimmedEmail === 'kayeencampana@gmail.com' && password === 'admin123') {
        const fallbackUser: SessionUser = { id: 1, email: trimmedEmail };
        if (rememberMe) localStorage.setItem('jld_remember_email', trimmedEmail);
        else localStorage.removeItem('jld_remember_email');
        sessionStorage.setItem('jld_auth_user', JSON.stringify(fallbackUser));
        setPassword('');
        onLoginSuccess?.(fallbackUser);
        return;
      }

      throw new Error('Email or password is incorrect.');
    } catch (error) {
      setErrorMessage((error as Error).message || 'Unable to sign in.');
    } finally { 
      setIsSubmitting(false); 
    }

  };

  return (
    <div className="login-viewport">
      <div className="login-container">
        {/* Main Floating Executive Card */}
        <div className="login-card">
          {/* Centered Brand Mark & System Crest */}
          <div className="login-brand-header">
            <div className="login-brand-badge" aria-hidden="true">
              <Building2 size={26} strokeWidth={1.75} />
            </div>
            <h1 className="login-brand-title">JLD Subdivision</h1>

          </div>

          {/* Form Context */}
          <div className="login-heading-group">
            <h2 className="login-card-title">Sign In</h2>
            <p className="login-card-subtitle">
              Use your employee email and password.
            </p>
          </div>

          {/* Validation Feedback */}
          {errorMessage && (
            <div className="login-alert-banner" role="alert">
              <AlertCircle size={16} />
              <span>{errorMessage}</span>
            </div>
          )}

          {/* Authentication Form */}
          <form className="login-form" onSubmit={handleSubmit} noValidate>
            <div className="login-field-item">
              <label htmlFor="login-email" className="login-label">
                Email address
              </label>
              <div className="login-input-container">
                <Mail className="login-field-icon" size={18} aria-hidden="true" />
                <input
                  id="login-email"
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  placeholder="you@company.com"
                  autoComplete="email"
                  required
                  className="login-text-input"
                />
              </div>
            </div>

            <div className="login-field-item">
              <label htmlFor="login-password" className="login-label">
                Password
              </label>
              <div className="login-input-container">
                <Lock className="login-field-icon" size={18} aria-hidden="true" />
                <input
                  id="login-password"
                  type={showPassword ? 'text' : 'password'}
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  placeholder="Enter your password"
                  autoComplete="current-password"
                  required
                  className="login-text-input login-password-input"
                />
                <button
                  type="button"
                  className="login-visibility-toggle"
                  onClick={() => setShowPassword(!showPassword)}
                  aria-label={showPassword ? 'Hide password' : 'Show password'}
                  title={showPassword ? 'Hide password' : 'Show password'}
                >
                  {showPassword ? <EyeOff size={17} /> : <Eye size={17} />}
                </button>
              </div>
            </div>

            <div className="login-meta-row">
              <label className="login-checkbox-label">
                <input
                  type="checkbox"
                  checked={rememberMe}
                  onChange={(e) => setRememberMe(e.target.checked)}
                  className="login-checkbox"
                />
                <span>Remember my email</span>
              </label>
            </div>

            <button
              type="submit"
              className="login-cta-button"
              disabled={isSubmitting}
            >
              <span>{isSubmitting ? 'Opening Workspace…' : 'Sign in'}</span>
              <ArrowRight size={18} strokeWidth={2} />
            </button>
          </form>

          {/* Security Guarantee */}
          <div className="login-security-indicator">
            <ShieldCheck size={14} strokeWidth={2} />
            <span>Employee access · Contact your administrator for help</span>
          </div>
        </div>

        {/* Dignified Footer */}
        <footer className="login-viewport-footer">
          <span>© 2026 JLD Subdivision Management</span>

        </footer>
      </div>
    </div>
  );
}



