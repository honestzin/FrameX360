using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

[DesignerCategory("Code")]
public class AdvancedTextBox : Control
{
    private const int MAX_LENGTH = 32767;
    private const float CURSOR_BLINK_SPEED = 0.8f;
    private const int ANIMATION_FPS = 144;

    private string _text = "";
    private int _cursorPos = 0;
    private int _selectionStart = 0;
    private int _selectionEnd = 0;
    private float _cursorAnim = 0.0f;
    private bool _cursorVisible = true;
    private Timer _animationTimer;
    private float _cursorX = 0;
    private float _cursorTargetX = 0;
    private float _textOffset = 0;
    private float _targetTextOffset = 0;
    private bool _isTextOverflowing = false;
    private bool _isHovered = false;
    private DateTime _lastInteractionTime = DateTime.Now;

    private Color _borderColor = Color.FromArgb(100, 100, 100);
    private Color _borderFocusColor = Color.DodgerBlue;
    private Color _backgroundColor = Color.FromArgb(40, 40, 40);
    private Color _textColor = Color.White;
    private Color _selectionColor = Color.FromArgb(80, 80, 80, 75);
    private Color _disabledColor = Color.FromArgb(100, 100, 100);
    private Color _hoverColor = Color.FromArgb(50, 50, 50);
    private Color _placeholderColor = Color.Gray;
    private string _placeholderText = "";
    private int _borderWidth = 1;
    private int _borderRadius = 0;
    private bool _useSystemPasswordChar = false;
    private char _passwordChar = '•';
    private int _cursorHeight = 0;

    public AdvancedTextBox()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.SupportsTransparentBackColor, true);

        _animationTimer = new Timer();
        _animationTimer.Interval = 1000 / ANIMATION_FPS;
        _animationTimer.Tick += AnimationTick;
        _animationTimer.Start();

        Font = new Font("Segoe UI", 9.75f);
        BackColor = _backgroundColor;
        ForeColor = _textColor;
        Cursor = Cursors.IBeam;
        Size = new Size(200, 30);
        Padding = new Padding(5);
    }

    [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
    public override string Text
    {
        get { return _text; }
        set
        {
            if (value != null && value.Length > MAX_LENGTH)
                value = value.Substring(0, MAX_LENGTH);

            _text = value ?? "";
            _cursorPos = Math.Min(_cursorPos, _text.Length);
            ForceUpdateCursor();
            OnTextChanged(EventArgs.Empty);
            Invalidate();
        }
    }

    public string PlaceholderText
    {
        get { return _placeholderText; }
        set { _placeholderText = value ?? ""; Invalidate(); }
    }

    public Color PlaceholderColor
    {
        get { return _placeholderColor; }
        set { _placeholderColor = value; Invalidate(); }
    }

    public Color BorderColor
    {
        get { return _borderColor; }
        set { _borderColor = value; Invalidate(); }
    }

    public Color BorderFocusColor
    {
        get { return _borderFocusColor; }
        set { _borderFocusColor = value; Invalidate(); }
    }

    [DefaultValue(1)]
    public int BorderWidth
    {
        get { return _borderWidth; }
        set { _borderWidth = Math.Max(0, value); Invalidate(); }
    }

    [DefaultValue(0)]
    public int BorderRadius
    {
        get { return _borderRadius; }
        set { _borderRadius = Math.Max(0, value); Invalidate(); }
    }

    public Color BackgroundColor
    {
        get { return _backgroundColor; }
        set { _backgroundColor = value; Invalidate(); }
    }

    public override Color ForeColor
    {
        get { return _textColor; }
        set { _textColor = value; Invalidate(); }
    }

    public Color HoverColor
    {
        get { return _hoverColor; }
        set { _hoverColor = value; Invalidate(); }
    }

    public Color SelectionColor
    {
        get { return _selectionColor; }
        set { _selectionColor = value; Invalidate(); }
    }

    public Color DisabledColor
    {
        get { return _disabledColor; }
        set { _disabledColor = value; Invalidate(); }
    }

    [DefaultValue(false)]
    public bool UseSystemPasswordChar
    {
        get { return _useSystemPasswordChar; }
        set { _useSystemPasswordChar = value; Invalidate(); }
    }

    [DefaultValue('•')]
    public char PasswordChar
    {
        get { return _passwordChar; }
        set { _passwordChar = value; Invalidate(); }
    }

    [DefaultValue(0)]
    public int CursorHeight
    {
        get { return _cursorHeight; }
        set { _cursorHeight = Math.Max(0, value); Invalidate(); }
    }

    [DefaultValue(false)]
    public bool ReadOnly { get; set; } = false;

    [DefaultValue(32767)]
    public int MaxLength { get; set; } = MAX_LENGTH;

    [Browsable(false)]
    public int SelectionStart
    {
        get { return _selectionStart; }
        set
        {
            _selectionStart = Math.Max(0, Math.Min(value, _text.Length));
            Invalidate();
        }
    }

    [Browsable(false)]
    public int SelectionLength
    {
        get { return Math.Abs(_selectionEnd - _selectionStart); }
        set
        {
            _selectionEnd = Math.Min(_selectionStart + value, _text.Length);
            Invalidate();
        }
    }

    public event EventHandler Accepted;

    public void Select(int start, int length)
    {
        _selectionStart = Math.Max(0, Math.Min(start, _text.Length));
        _selectionEnd = Math.Min(_selectionStart + length, _text.Length);
        _cursorPos = _selectionEnd;
        ForceUpdateCursor();
    }

    public void SelectAll()
    {
        _selectionStart = 0;
        _selectionEnd = _text.Length;
        _cursorPos = _text.Length;
        ForceUpdateCursor();
    }

    protected virtual void OnAccepted()
    {
        if (Accepted != null)
            Accepted(this, EventArgs.Empty);
    }

    private void AnimationTick(object sender, EventArgs e)
    {
        _cursorAnim += 1.0f / ANIMATION_FPS;
        bool shouldBlink = (_cursorAnim % CURSOR_BLINK_SPEED) <= (CURSOR_BLINK_SPEED * 0.5f);

        if (!shouldBlink && (IsUserInteracting() || _cursorAnim < 0.1f))
            shouldBlink = true;

        if (_cursorVisible != shouldBlink)
        {
            _cursorVisible = shouldBlink;
            Invalidate();
        }

        _cursorX = Lerp(_cursorX, _cursorTargetX, 0.3f);
        _textOffset = Lerp(_textOffset, _targetTextOffset, 0.3f);

        if (Math.Abs(_cursorX - _cursorTargetX) > 0.5f || Math.Abs(_textOffset - _targetTextOffset) > 0.5f)
            Invalidate();
    }

    private bool IsUserInteracting()
    {
        return (DateTime.Now - _lastInteractionTime).TotalSeconds < 0.5;
    }

    private float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }

    private string GetDisplayText()
    {
        if (string.IsNullOrEmpty(_text)) return "";
        return _useSystemPasswordChar ? new string(_passwordChar, _text.Length) : _text;
    }

    private void DrawRoundedRectangle(Graphics g, Rectangle rect, int radius, Pen pen)
    {
        if (radius <= 0)
        {
            g.DrawRectangle(pen, rect);
            return;
        }
        GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
        try
        {
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            g.DrawPath(pen, path);
        }
        finally
        {
            path.Dispose();
        }
    }

    private void FillRoundedRectangle(Graphics g, Rectangle rect, int radius, Brush brush)
    {
        if (radius <= 0)
        {
            g.FillRectangle(brush, rect);
            return;
        }
        GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
        try
        {
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            g.FillPath(brush, path);
        }
        finally
        {
            path.Dispose();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (!IsHandleCreated || !Visible) return;

        Graphics g = e.Graphics;
        try
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.CompositingMode = CompositingMode.SourceOver;
            g.PixelOffsetMode = PixelOffsetMode.None;

            Rectangle rect = ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                return;
            }

            Rectangle borderRect = new Rectangle(0, 0, rect.Width - 1, rect.Height - 1);
            Rectangle innerRect = new Rectangle(
                _borderWidth,
                _borderWidth,
                rect.Width - _borderWidth * 2 - 1,
                rect.Height - _borderWidth * 2 - 1);
            if (innerRect.Width <= 0 || innerRect.Height <= 0)
            {
                return;
            }

            SolidBrush bgBrush = new SolidBrush(_isHovered ? _hoverColor : _backgroundColor);
            try
            {
                FillRoundedRectangle(g, innerRect, _borderRadius, bgBrush);
            }
            finally
            {
                bgBrush.Dispose();
            }

            Pen borderPen = new Pen(Focused ? _borderFocusColor : _borderColor, _borderWidth);
            try
            {
                DrawRoundedRectangle(g, borderRect, _borderRadius, borderPen);
            }
            finally
            {
                borderPen.Dispose();
            }

            Rectangle textRect = new Rectangle(
                Padding.Left + _borderWidth,
                Padding.Top + _borderWidth,
                rect.Width - Padding.Horizontal - _borderWidth * 2,
                rect.Height - Padding.Vertical - _borderWidth * 2);
            if (textRect.Width <= 0 || textRect.Height <= 0)
            {
                return;
            }

            string displayText = GetDisplayText();
            bool showPlaceholder = string.IsNullOrEmpty(_text) && !string.IsNullOrEmpty(_placeholderText) && !Focused;

            string measureText = showPlaceholder ? _placeholderText : (string.IsNullOrEmpty(displayText) ? "A" : displayText);
            SizeF textSize = g.MeasureString(measureText, Font);
            float textY = textRect.Y + (textRect.Height - textSize.Height) / 2;

            _isTextOverflowing = textSize.Width > textRect.Width;

            if (Focused && _selectionStart != _selectionEnd && !showPlaceholder)
                DrawTextSelection(g, textRect, displayText, textY);

            SolidBrush textBrush = new SolidBrush(showPlaceholder ? _placeholderColor : Enabled ? _textColor : _disabledColor);
            try
            {
                if (_isTextOverflowing && Focused)
                {
                    g.SetClip(textRect);
                    try
                    {
                        g.DrawString(showPlaceholder ? _placeholderText : displayText, Font, textBrush,
                            textRect.X + _textOffset, textY);
                    }
                    finally
                    {
                        g.ResetClip();
                    }
                }
                else
                {
                    g.DrawString(showPlaceholder ? _placeholderText : displayText, Font, textBrush,
                        textRect.X, textY);
                }
            }
            finally
            {
                textBrush.Dispose();
            }

            if (Focused && !ReadOnly && (_cursorVisible || IsUserInteracting()))
            {
                UpdateCursorTargetPosition(g, textRect, displayText, textY);
                DrawCursor(g, textRect, displayText, textY);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void DrawTextSelection(Graphics g, Rectangle textRect, string displayText, float textY)
    {
        if (_selectionStart == _selectionEnd || _selectionStart < 0 || _selectionEnd > displayText.Length) return;

        int selStart = Math.Min(_selectionStart, _selectionEnd);
        int selEnd = Math.Max(_selectionStart, _selectionEnd);
        selEnd = Math.Min(selEnd, displayText.Length);

        string textBefore = displayText.Substring(0, selStart);
        string selectedText = displayText.Substring(selStart, selEnd - selStart);

        StringFormat format = StringFormat.GenericTypographic;
        format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

        SizeF beforeSize = g.MeasureString(textBefore, Font, int.MaxValue, format);
        SizeF selectedSize = g.MeasureString(selectedText, Font, int.MaxValue, format);

        float selectionX = textRect.X + (float)Math.Round(beforeSize.Width) + _textOffset;
        float selectionWidth = (float)Math.Round(selectedSize.Width) + 4.3f;

        if (selectionX + selectionWidth > textRect.Right)
            selectionWidth = textRect.Right - selectionX;
        if (selectionX < textRect.Left)
        {
            selectionWidth -= (textRect.Left - selectionX);
            selectionX = textRect.Left;
        }

        if (selectionWidth > 0)
        {
            RectangleF selectionRect = new RectangleF(
                selectionX,
                textY,
                selectionWidth,
                textRect.Height);
            SolidBrush selectionBrush = new SolidBrush(_selectionColor);
            try
            {
                g.FillRectangle(selectionBrush, selectionRect);
            }
            finally
            {
                selectionBrush.Dispose();
            }
        }
    }

    private void UpdateCursorTargetPosition(Graphics g, Rectangle textRect, string displayText, float textY)
    {
        try
        {
            if (string.IsNullOrEmpty(displayText))
            {
                _cursorTargetX = textRect.X;
                _targetTextOffset = 0;
                return;
            }

            int safePos = Math.Min(_cursorPos, displayText.Length);
            if (safePos < 0) safePos = 0;
            string textBefore = displayText.Substring(0, safePos);

            StringFormat format = StringFormat.GenericTypographic;
            format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

            SizeF beforeSize = g.MeasureString(textBefore, Font, int.MaxValue, format);
            float absoluteCursorX = textRect.X + beforeSize.Width;

            bool shouldApplyOffset =
                _cursorPos == displayText.Length ||
                (_cursorPos > _selectionStart && _cursorPos > _selectionEnd);

            if (shouldApplyOffset)
                absoluteCursorX += 3;

            SizeF totalTextSize = g.MeasureString(displayText, Font, int.MaxValue, format);
            _isTextOverflowing = totalTextSize.Width > textRect.Width;

            if (_isTextOverflowing && Focused)
            {
                float cursorMargin = 2f;
                float visibleRightEdge = textRect.Right - cursorMargin;

                if (absoluteCursorX > visibleRightEdge)
                {
                    _targetTextOffset = visibleRightEdge - absoluteCursorX;
                }
                else if (absoluteCursorX < textRect.Left + cursorMargin)
                {
                    _targetTextOffset = (textRect.Left + cursorMargin) - absoluteCursorX;
                }
                else
                {
                    _targetTextOffset = 0;
                }

                float maxOffset = textRect.Width - totalTextSize.Width - cursorMargin;
                _targetTextOffset = Math.Max(maxOffset, Math.Min(0, _targetTextOffset));
            }
            else
            {
                _targetTextOffset = 0;
            }

            _cursorTargetX = absoluteCursorX + _textOffset;
        }
        catch (Exception ex)
        {
            _cursorPos = 0;
            _cursorX = textRect.X;
            _cursorTargetX = textRect.X;
            _targetTextOffset = 0;
        }
    }

    private void DrawCursor(Graphics g, Rectangle textRect, string displayText, float textY)
    {
        float actualX = Math.Max(textRect.Left, Math.Min(textRect.Right - 1, _cursorX));
        float cursorHeight = _cursorHeight > 0 ? _cursorHeight : g.MeasureString("A", Font).Height;
        float top = textY;
        float bottom = top + cursorHeight;

        Pen cursorPen = new Pen(_textColor, 1.5f);
        try
        {
            g.DrawLine(cursorPen, actualX, top, actualX, bottom);
        }
        finally
        {
            cursorPen.Dispose();
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            Focus();
            UpdateCursorPositionFromPoint(e.Location);
            _selectionStart = _selectionEnd = _cursorPos;
            ForceUpdateCursor();
            _lastInteractionTime = DateTime.Now;
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        bool wasHovered = _isHovered;
        _isHovered = ClientRectangle.Contains(e.Location);
        if (wasHovered != _isHovered) Invalidate();

        if (e.Button == MouseButtons.Left && Focused)
        {
            UpdateCursorPositionFromPoint(e.Location);
            _selectionEnd = _cursorPos;
            ForceUpdateCursor();
            _lastInteractionTime = DateTime.Now;
        }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _isHovered = false;
        Invalidate();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (ReadOnly) return;

        bool shift = (ModifierKeys & Keys.Shift) != 0;
        bool ctrl = (ModifierKeys & Keys.Control) != 0;

        ForceUpdateCursor();

        switch (e.KeyCode)
        {
            case Keys.Left: HandleLeftKey(shift, ctrl); break;
            case Keys.Right: HandleRightKey(shift, ctrl); break;
            case Keys.Home: HandleHomeKey(shift); break;
            case Keys.End: HandleEndKey(shift); break;
            case Keys.Back: HandleBackspaceKey(); break;
            case Keys.Delete: HandleDeleteKey(); break;
            case Keys.A: if (ctrl) SelectAll(); break;
            case Keys.C: if (ctrl) CopyToClipboard(); break;
            case Keys.X: if (ctrl) CutToClipboard(); break;
            case Keys.V: if (ctrl) PasteFromClipboard(); break;
            case Keys.Enter: OnAccepted(); break;
        }

        _lastInteractionTime = DateTime.Now;
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);
        if (ReadOnly) return;
        if (e.KeyChar >= 32 || e.KeyChar == 9)
        {
            if (_selectionStart != _selectionEnd) DeleteSelection();
            InsertText(e.KeyChar.ToString());
            _lastInteractionTime = DateTime.Now;
        }
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        ForceUpdateCursor();
        _lastInteractionTime = DateTime.Now;
        Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        _cursorVisible = false;
        Invalidate();
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Invalidate();
    }

    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        ForceUpdateCursor();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_animationTimer != null)
                _animationTimer.Dispose();
        }
        base.Dispose(disposing);
    }

    private void HandleLeftKey(bool shift, bool ctrl)
    {
        if (_cursorPos > 0)
        {
            _cursorPos = ctrl ? FindPrevWordBoundary(_cursorPos) : _cursorPos - 1;
            if (!shift) _selectionStart = _selectionEnd = _cursorPos;
            else _selectionStart = Math.Min(_selectionStart, _cursorPos);
        }
        else if (!shift) _selectionStart = _selectionEnd = _cursorPos;
        ForceUpdateCursor();
    }

    private void HandleRightKey(bool shift, bool ctrl)
    {
        if (_cursorPos < _text.Length)
        {
            _cursorPos = ctrl ? FindNextWordBoundary(_cursorPos) : _cursorPos + 1;
            if (!shift) _selectionStart = _selectionEnd = _cursorPos;
            else _selectionEnd = Math.Max(_selectionEnd, _cursorPos);
        }
        else if (!shift) _selectionStart = _selectionEnd = _cursorPos;
        ForceUpdateCursor();
    }

    private void HandleHomeKey(bool shift)
    {
        _cursorPos = 0;
        if (!shift) _selectionStart = _selectionEnd = _cursorPos;
        else _selectionStart = 0;
        ForceUpdateCursor();
    }

    private void HandleEndKey(bool shift)
    {
        _cursorPos = _text.Length;
        if (!shift) _selectionStart = _selectionEnd = _cursorPos;
        else _selectionEnd = _text.Length;
        ForceUpdateCursor();
    }

    private void HandleBackspaceKey()
    {
        if (_selectionStart != _selectionEnd)
            DeleteSelection();
        else if (_cursorPos > 0)
        {
            _text = _text.Remove(_cursorPos - 1, 1);
            _cursorPos--;
            OnTextChanged(EventArgs.Empty);
            ForceFullUpdate();
        }
    }

    private void HandleDeleteKey()
    {
        if (_selectionStart != _selectionEnd)
            DeleteSelection();
        else if (_cursorPos < _text.Length)
        {
            _text = _text.Remove(_cursorPos, 1);
            OnTextChanged(EventArgs.Empty);
            ForceUpdateCursor();
            Invalidate();
        }
    }

    private void InsertText(string text)
    {
        if (_cursorPos <= _text.Length && _text.Length + text.Length <= MaxLength)
        {
            _text = _text.Insert(_cursorPos, text);
            _cursorPos += text.Length;
            _selectionStart = _selectionEnd = _cursorPos;
            OnTextChanged(EventArgs.Empty);
            ForceUpdateCursor();
        }
    }

    private void DeleteSelection()
    {
        if (_selectionStart == _selectionEnd) return;
        int start = Math.Min(_selectionStart, _selectionEnd);
        int end = Math.Max(_selectionStart, _selectionEnd);
        if (start < 0 || end > _text.Length) return;
        _text = _text.Remove(start, end - start);
        _cursorPos = start;
        _selectionStart = _selectionEnd = _cursorPos;
        _textOffset = _targetTextOffset = 0;
        OnTextChanged(EventArgs.Empty);
        ForceUpdateCursor();
        Invalidate();
    }

    private int FindPrevWordBoundary(int pos)
    {
        if (pos <= 0) return 0;
        while (pos > 0 && char.IsWhiteSpace(_text[pos - 1])) pos--;
        while (pos > 0 && !char.IsWhiteSpace(_text[pos - 1])) pos--;
        return pos;
    }

    private int FindNextWordBoundary(int pos)
    {
        if (pos >= _text.Length) return _text.Length;
        while (pos < _text.Length && !char.IsWhiteSpace(_text[pos])) pos++;
        while (pos < _text.Length && char.IsWhiteSpace(_text[pos])) pos++;
        return pos;
    }

    private void UpdateCursorPositionFromPoint(Point point)
    {
        if (!IsHandleCreated || !Visible) return;
        Graphics g = CreateGraphics();
        try
        {
            string displayText = GetDisplayText();
            float clickX = point.X - Padding.Left - _borderWidth - _textOffset;
            int low = 0, high = displayText.Length, best = 0;
            float bestDist = float.MaxValue;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                string substr = displayText.Substring(0, mid);
                float width = g.MeasureString(substr, Font).Width;
                float dist = Math.Abs(width - clickX);

                if (dist < bestDist)
                {
                    bestDist = dist;
                    best = mid;
                }

                if (width < clickX) low = mid + 1;
                else high = mid - 1;
            }

            _cursorPos = best;
            ForceUpdateCursor();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            g.Dispose();
        }
    }

    private void ForceUpdateCursor()
    {
        if (!IsHandleCreated || !Visible) return;
        Graphics g = CreateGraphics();
        try
        {
            Rectangle textRect = new Rectangle(
                Padding.Left + _borderWidth,
                Padding.Top + _borderWidth,
                Width - Padding.Horizontal - _borderWidth * 2,
                Height - Padding.Vertical - _borderWidth * 2);
            if (textRect.Width <= 0 || textRect.Height <= 0)
            {
                return;
            }

            string displayText = GetDisplayText();
            string measureText = string.IsNullOrEmpty(displayText) ? "A" : displayText;
            SizeF textSize = g.MeasureString(measureText, Font);
            float textY = textRect.Y + (textRect.Height - textSize.Height) / 2;

            UpdateCursorTargetPosition(g, textRect, displayText, textY);
        }
        catch (Exception ex)
        {
        }
        finally
        {
            g.Dispose();
        }
        ResetCursorAnimation();
        Invalidate();
    }

    private void ForceFullUpdate()
    {
        _textOffset = _targetTextOffset = 0;
        ForceUpdateCursor();
    }

    private void ResetCursorAnimation()
    {
        _cursorAnim = 0.0f;
        _cursorVisible = true;
    }

    private void CopyToClipboard()
    {
        if (_selectionStart == _selectionEnd) return;
        int start = Math.Min(_selectionStart, _selectionEnd);
        int end = Math.Max(_selectionStart, _selectionEnd);
        if (start < 0 || end > _text.Length) return;
        try { Clipboard.SetText(_text.Substring(start, end - start)); } catch { }
    }

    private void CutToClipboard()
    {
        if (_selectionStart != _selectionEnd)
        {
            CopyToClipboard();
            DeleteSelection();
        }
    }

    private void PasteFromClipboard()
    {
        if (ReadOnly) return;
        try
        {
            if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();
                if (text.Length + _text.Length > MaxLength)
                    text = text.Substring(0, MaxLength - _text.Length);
                InsertText(text);
            }
        }
        catch { }
    }
}