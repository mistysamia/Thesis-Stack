class Monkey extends React.Component {
    render() {
        return (
            <div>
                <h1>This is Monkey component {this.props.author}</h1>
            </div>
        )
    }
}

React.render(<Monkey />, document.getElementById("paper"));