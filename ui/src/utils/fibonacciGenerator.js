const fibonacci_series = function (n)
{
    if (n===1)
    {
        return [0, 1];
    }
    else
    {
        const s = fibonacci_series(n - 1);
        s.push(s[s.length - 1] + s[s.length - 2]);
        return s;
    }
};

export default fibonacci_series;